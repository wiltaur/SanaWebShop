import React, { Component } from "react";
import 'react-bootstrap-table-nextgen/dist/react-bootstrap-table-nextgen.min.css';
import 'react-bootstrap-table-nextgen-paginator/dist/react-bootstrap-table-nextgen-paginator.min.css';
import 'bootstrap/dist/css/bootstrap.css';
import {Form, Button, Col , Row} from 'react-bootstrap';
import BootstrapTable from 'react-bootstrap-table-nextgen';
import cellEditFactory from 'react-bootstrap-table-nextgen-editor';
// import paginationFactory from 'react-bootstrap-table-nextgen-paginator';
import Pagination from 'react-bootstrap/Pagination';
import { variables } from '../Variables';

interface CustomerSession {
  id: number;
  name: string;
}

export class Catalog extends Component<{}, { [key: string]: any}>{

  constructor(props:any) {
    super(props);
    this.state = {
      rowCount: 0,
      validated: false,
      customer: '',
      isCustomerOk: false,
      idCustomer: null,
      totalPages: null,
      pageNumber:1,
      itemsShopf:[],
      products: [],
      productsTemp: []
    }
  }

 async componentDidMount() {
    var customerSaved = sessionStorage.getItem("customer");
    if(customerSaved){
      let customerSession: CustomerSession = JSON.parse(customerSaved);
      this.setState({ idCustomer: customerSession.id, customer: customerSession.name, isCustomerOk:true});
    }
    await this.GetProducts(this.state.pageNumber);
  }

  createShoppingCart = async (event: any) =>  {
    event.preventDefault();
    event.stopPropagation();
    const bodyReq = {user: this.state.idCustomer, products: this.state.productsTemp};
    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: this.fortmatResponse(bodyReq)
    };
    await fetch(`${variables.API_URL}Order/CreateShoppingCart`, requestOptions)
      .then(response => response.json())
      .then(data => {
        if(data.isSuccess){
          alert("Shopping Cart Created: ");
          this.setState({ productsTemp: []});
          sessionStorage.setItem("orderId", data.data);
          window.location.reload();
        }
        else{alert(data.data)
        }
    });
  }

  createCustomer() {
    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' }
      // body: JSON.stringify({ name: 'WILSON' })
    };
    if (this.state.customer) {
      fetch(`${variables.API_URL}Customer/Create?name=${this.state.customer}`, requestOptions)
              .then(response => response.json())
              .then(data => {
                if(data.isSuccess){
                  this.setState({ idCustomer: data.data, isCustomerOk:true});
                  sessionStorage.setItem("customer",  this.fortmatResponse({ id:data.data, name: this.state.customer }));
                }
                else{alert(data.data)
                }
            });
    }
  }

  async GetProducts(pageNum : any) {
    const requestOptions = {
      method: 'GET',
      headers: { 'Content-Type': 'application/json' }
    };
    // try {
      await fetch(`${variables.API_URL}Product/GetProductsDetail?PageNumber=${pageNum}`, requestOptions)
      .then(response => response.json())
      .then(data => {
      if(data.isSuccess){
        this.state.products.splice(0, this.state.products.length);
        data.data.products.forEach((e: { code: any; title: any; description: any; price: any; stock: any; }) => {
          let product = {
            code: e.code,
            title: e.title,
            description: e.description,
            price: e.price,
            stock: e.stock,
            quantity: this.getQuantity(e.code)
          };
          this.state.products.push(product);            
        });
        this.setState({ rowCount: data.data.totalRecords, totalPages: data.data.totalPages, pageNumber: data.data.pageNumber });
      }else{
        alert(data.data);
      }
    });
  }

  handleDataChange = ({dataSize}:any) => {
    this.setState({ rowCount: dataSize });
  }

  handleSubmit = async (event: any) => {
    const form = event.currentTarget;
    event.preventDefault();
    if (form.checkValidity() === false) {
      event.stopPropagation();
      alert("Please provide a valid name.");
    }else{
      this.createCustomer();
    }
    this.setState({ validated: true });
  }

  fortmatResponse = (res: any) => {
    return JSON.stringify(res, null, 2);
  }

  handleChange = (e: any) => {
    let updatedValue = '';
    updatedValue = e.target.value;
    this.setState({ customer: updatedValue });
  }

  paginationClicked = (event:any) => {
    event.preventDefault();
    event.stopPropagation();
    var itemClicked = event.target.text;
    this.GetProducts(itemClicked);
  }

  getQuantity( code:any){
    let result = 0;
    this.state.productsTemp.forEach( (item:any) => {
      if(item.code === code){
        result = item.quantity;
        return false;
      }
    });
    return result;
  }

  deleteProduct( code:any){
    this.state.productsTemp.forEach( (item:any, index:any) => {
      if(item.code === code)
        this.state.productsTemp.splice(index,1);
    });
  }

  tempShopCart(){
    if(this.state.productsTemp){
      let itemsShop = [];
      itemsShop.push(
        <h5>Your Shopping Cart: </h5>
      );
      for (let number = 0; number < this.state.productsTemp.length; number++) {
        itemsShop.push(
          <span className="badge">{this.state.productsTemp[number].code+ " ("+this.state.productsTemp[number].quantity+")" }</span>
        );
      }
      this.setState({ itemsShopf: itemsShop});
    }
  }

  render() {

    const columns = [{
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
        if (newValue < 0 || newValue === null || newValue === '') {
          return {
            valid: false,
            message: 'Quantity should bigger than 0'
          };
        }
        if(newValue === '0'){
          this.deleteProduct(row.code);
        }else{
          this.deleteProduct(row.code);
          productsTemp.push({code:row.code, quantity:newValue});
        }
        this.tempShopCart();
        return true;
      }
    }];
  
    const {
      rowCount,
      validated,
      customer,
      isCustomerOk,
      totalPages,
      pageNumber,
      itemsShopf,
      products,
      productsTemp
    } = this.state;

    let active = pageNumber;
    let items = [];
    for (let number = 1; number <= totalPages; number++) {
      items.push(
        <Pagination.Item key={number} active={number === active} onClick={(event) => this.paginationClicked(event)}>
          {number}
        </Pagination.Item>
      );
    }

    const paginationBasic = (
      <>
      <div>Showing <b>{pageNumber*10-9}</b> to <b>{rowCount > pageNumber*10 ? pageNumber*10 : rowCount}</b> of <b>{rowCount}</b> Results</div>
      <div>
        <Pagination>{items}</Pagination>
      </div></>
    );

    return (
      <>
      <br />
      <Form noValidate validated={validated} onSubmit={this.handleSubmit}>
        <Row className="mb-3 ButCatalog">
          <Form.Group as={Col} md="4" controlId="fCustomer">
            <Form.Label>Customer</Form.Label>
            <Form.Control readOnly={isCustomerOk} defaultValue={customer} onChange={(e) => this.handleChange(e)} type="text" placeholder="Enter Customer Name" required />
            {/* <Form.Control.Feedback type="invalid">
              Please provide a valid name.
            </Form.Control.Feedback> */}
          </Form.Group>
        </Row>
        <Button className="ButCatalog1" variant="primary" type="submit" disabled={isCustomerOk}>
          Save
        </Button>
      </Form>
      <center>
        <h1>Catalog</h1>
      </center>
      {itemsShopf}
      <BootstrapTable 
        onDataSizeChange={ this.handleDataChange }
        keyField='code' 
        data={ products } 
        columns={ columns }
        noDataIndication="Table is Empty"
        cellEdit={ cellEditFactory({ mode: 'click',
          blurToSave: true })}
        />
        <center>
          {paginationBasic}
          <Button disabled={!isCustomerOk || productsTemp.length===0} onClick={this.createShoppingCart}>Create Shopping Cart</Button>
        </center>
      </>
    );
  }
}