import React from 'react';
import logo from './logo.svg';
import './App.css';
import { Catalog } from './services/Catalog';
import { BrowserRouter, Route, NavLink, Routes } from 'react-router-dom';
import { ShoppingCart } from './services/ShoppingCart';
import { Home } from './Home';
// import { PostRequest } from './services/SaveCustomer';

function App() {
  return (
    <><div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <h3 className="d-flex justify-content-center m-3">
          SANA Web Shop
        </h3>
      </header>
    </div><BrowserRouter>
      <div className="App container">
        <nav className="navbar navbar-expand-sm bg-light navbar-dark">
          <ul className="navbar-nav">
            <li className="nav-item m-1">
              <NavLink className="btn btn-light btn-outline-primary" to="/home">
                Home
              </NavLink>
            </li>
            <li className="nav-item m-1">
              <NavLink className="btn btn-light btn-outline-primary" to="/catalog">
                Catalog
              </NavLink>
            </li>
            <li className="nav-item m-1">
              <NavLink className="btn btn-light btn-outline-primary" to="/shopping-cart">
                ShoppingCart
              </NavLink>
            </li>
          </ul>
        </nav>
        <Routes>
          <Route path='/' element={<Home />}></Route>
          <Route path='/home' element={<Home />}></Route>
          <Route path='/catalog' element={<Catalog />}></Route>
          <Route path='/shopping-cart' element={<ShoppingCart />}></Route>
        </Routes>
      </div>
    </BrowserRouter></>
  );
}

export default App;