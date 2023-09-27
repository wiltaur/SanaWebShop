import React, { Component } from "react";
import 'react-bootstrap-table-nextgen/dist/react-bootstrap-table-nextgen.min.css';
import 'react-bootstrap-table-nextgen-paginator/dist/react-bootstrap-table-nextgen-paginator.min.css';
import 'bootstrap/dist/css/bootstrap.css';
import { Button} from 'react-bootstrap';
import BootstrapTable from 'react-bootstrap-table-nextgen';
import cellEditFactory from 'react-bootstrap-table-nextgen-editor';
import { variables } from "../Variables";

interface CustomerSession {
  id: number;
  name: string;
}

export class ShoppingCart extends Component<{}, { [key: string]: any}>{
  constructor(props:any) {
    super(props);
    this.state = {
      rowCount: 0,
      validated: false,
      customer: '',
      idCustomer: null,
      totalPages: null,
      pageNumber:1,
      itemsShopf:[],
      products: [],
      productsTemp: [],
      itemsTotalShopf: null,
      itemsTotalShopf2: null
    }
  }

 async componentDidMount() {
    await this.GetProducts();
  }

  processOrder = async (event: any) =>  {
    event.preventDefault();
    event.stopPropagation();

    const bodyReq = {user: this.state.idCustomer, products: this.state.productsTemp};
    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: this.fortmatResponse(bodyReq)
    };
    await fetch(`${variables.API_URL}Order/ProcessOrder/${sessionStorage.getItem('orderId')}`, requestOptions)
      .then(response => response.json())
      .then(data => {
        if(data.isSuccess){
          alert("Order processed!");
          sessionStorage.removeItem('orderId');
          window.location.reload();
        }
        else{alert(data.data)
        }
    });
  }

  async GetProducts() {
    const requestOptions = {
      method: 'GET',
      headers: { 'Content-Type': 'application/json' }
    };
    var customerSaved = sessionStorage.getItem("customer");
    var orderSaved = sessionStorage.getItem("orderId");
    if(customerSaved && orderSaved){
      let customerSession: CustomerSession = JSON.parse(customerSaved);

      await fetch(`${variables.API_URL}Order/GetShoppingCart?customer=${customerSession.id}&order=${orderSaved}`, requestOptions)
      .then(response => response.json())
      .then(data => {
        if(data.isSuccess){
          this.state.products.splice(0, this.state.products.length);
          data.data.forEach((e:any) => {
            let product = {
              id: e.idOrderDetail,
              code: e.code,
              title: e.title,
              description: e.description,
              price: e.price,
              stock: e.stock,
              quantity: e.quantity,
              total: e.price*e.quantity
            };
            this.state.products.push(product);
            this.state.productsTemp.push({id:product.id, quantity:product.quantity, price:product.price});
          });
          this.tempShopCart();
          this.setState({ rowCount: data.data.totalRecords, totalPages: data.data.totalPages, pageNumber: data.data.pageNumber });
        }else{
          alert(data.data);
        }
      });
    }
  }

  handleDataChange = ({dataSize}:any) => {
    this.setState({ rowCount: dataSize });
  }

  fortmatResponse = (res: any) => {
    return JSON.stringify(res, null, 2);
  }

  handleChange = (e: any) => {
    let updatedValue = '';
    updatedValue = e.target.value;
    this.setState({ customer: updatedValue });
  }

  getQuantity( id:any){
    let result = 0;
    this.state.productsTemp.forEach( (item:any) => {
      if(item.id === id){
        result = item.quantity;
        return false;
      }
    });
    return result;
  }

  deleteProduct( id:any){
    this.state.productsTemp.forEach( (item:any, index:any) => {
      if(item.id === id)
        this.state.productsTemp.splice(index,1);
    });
  }

  deleteProductLst( id:any){
    this.state.products.forEach( (item:any, index:any) => {
      if(item.id === id)
        this.state.products.splice(index,1);
    });
  }

  tempShopCart(){
    if(this.state.productsTemp){
      let totalItems = 0;
      let totalPrice = 0;

      for (let number = 0; number < this.state.productsTemp.length; number++) {
        totalItems+= Number(this.state.productsTemp[number].quantity);
        totalPrice+= this.state.productsTemp[number].quantity * this.state.productsTemp[number].price;
      }
      let itemsTotalShop = <span className="badge">{"("+totalItems+ " units)"}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{"$ "+totalPrice}</span>;
      let itemsTotalShop2 = <span className="badge">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{"$ "+totalPrice}</span>;
      this.setState({ itemsTotalShopf: itemsTotalShop});
      this.setState({ itemsTotalShopf2: itemsTotalShop2});
    }
  }

  handleDeleteRow = async (rowId:any) => {
    const requestOptions = {
      method: 'DELETE',
      headers: { 'Content-Type': 'application/json' }
    };
    await fetch(`${variables.API_URL}Order/DeleteFromShoppingCart/${rowId}`, requestOptions)
            .then(response => response.json())
            .then(data => {
              if(data.isSuccess){
                this.deleteProduct(rowId);
                this.deleteProductLst(rowId);
                alert("Product deleted from Shopping Cart.");
                this.setState({ 
                  products: this.state.products,  
                  productsTemp: this.state.productsTemp}
                );
                this.tempShopCart();
              }
              else{alert(data.data)
              }
    });
  };

  updateQuantityRow = async (idP:any, q:any) => {
    const bodyReq = {id: idP, quantity: q};
    const requestOptions = {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: this.fortmatResponse(bodyReq)
    };
    await fetch(`${variables.API_URL}Order/UpdateQuantity`, requestOptions)
      .then(response => response.json())
      .then(data => {
        if(!data.isSuccess){
          alert(data.data)
        }
    });
  };

  render() {

    const columns = [
      {
        dataField: 'id',
        text: 'Id',
        editable: false,
        hidden: true
      },{
      dataField: 'code',
      text: 'Product Code',
      editable: false
    }, {
      dataField: 'title',
      text: 'Title',
      editable: false
    }, {
      dataField: 'description',
      text: 'Description',
      editable: false
    }, {
      dataField: 'price',
      text: 'Price',
      editable: false
    }, {
      dataField: 'stock',
      text: 'Stock',
      editable: false
    }, {
      dataField: 'quantity',
      text: 'Quantity',
      validator: (newValue:any, row:any, column:any) => {
        if (isNaN(newValue)) {
          return {
            valid: false,
            message: 'Quantity should be numeric'
          };
        }
        if (newValue > row.stock) {
          return {
            valid: false,
            message: 'Quantity must not exceed the stock'
          };
        }
        if (newValue <= 0 || newValue === null || newValue === '') {
          return {
            valid: false,
            message: 'Quantity should bigger than 0'
          };
        }
        if(newValue === '0'){
          this.deleteProduct(row.id);
        }else{
          this.deleteProduct(row.id);
          productsTemp.push({id:row.id, quantity:newValue, price:row.price});
        }
        this.tempShopCart();
        row.total = newValue*row.price;
        this.updateQuantityRow(row.id, newValue);
        return true;
      }
    }, {
      dataField: 'total',
      text: 'Total',
      editable: false
    },
    {
      dataField: "remove",
      text: "",
      editable: false,
      formatter: (cellContent:any, row:any) => {
        return (
          <button
            className="btn btn-danger btn-xs"
            onClick={() => this.handleDeleteRow(row.id)}
          >
            Delete
          </button>
        );
      },
    },];

    const {
      products,
      productsTemp,
      itemsTotalShopf,
      itemsTotalShopf2
    } = this.state;

    return (
      <>
      <br />
      <center>
        <h1>My shopping cart</h1>
      </center>
      <BootstrapTable 
        onDataSizeChange={ this.handleDataChange }
        keyField='id' 
        data={ products } 
        columns={ columns }
        noDataIndication="Table is Empty"
        cellEdit={ cellEditFactory({ mode: 'click',
          blurToSave: true })}
      />
      <center>
        <h1>Shopping cart details</h1>
      </center>
      <div className="pShopping">
        <p>
          Items {itemsTotalShopf}
        </p>
        <p>  
          _____________________________________________
        </p>
        <p>
          Total {itemsTotalShopf2}
        </p>
        <Button disabled={productsTemp.length===0} onClick={this.processOrder}>Process Order</Button>
      </div>
      </>
    );
  }
}