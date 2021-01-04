import { Navbar,NavDropdown,Form,Button,FormControl,Nav} from 'react-bootstrap';
import React, { useState, useEffect } from 'react';

export default function Menu(props){
  const logout=()=>{
    sessionStorage.clear();
  }
    return (
<>
<Navbar collapseOnSelect expand="lg"  bg="dark" variant="dark">
    <Navbar.Brand href="#home">UrlShortener</Navbar.Brand>
    <Navbar.Toggle aria-controls="responsive-navbar-nav" />

    <Navbar.Collapse id="responsive-navbar-nav">
    <Nav className="mr-auto">
        {console.log(props)}
      {!props.isAuthenticated&&<Nav.Link href="/login" >Login</Nav.Link>}
      {!props.isAuthenticated&&<Nav.Link href="/register" >Register</Nav.Link>}

      {props.isAuthenticated&&<Nav.Link onClick={logout} href="/login" >Logout</Nav.Link>}
      {props.isAuthenticated&&<Nav.Link  href="/create-link" >Create Link</Nav.Link>}
      {props.isAuthenticated&&<Nav.Link  href="/link-list" >Your Links</Nav.Link>}



    </Nav>
    </Navbar.Collapse>

  </Navbar>
</>
    );
}