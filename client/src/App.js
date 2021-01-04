import logo from './logo.svg';
import { BrowserRouter as Router, Route ,Switch,Redirect} from "react-router-dom";
import Login from "./components/login.component";
import Navigation from "./components/navbar.component";
import CreateLinkPage from "./components/create-link-page.component";
import React, { useState, useEffect } from 'react';
import Register from "./components/register.component";
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';
import Axios from 'axios';

// import ProtectedRoute from "./components/ProtectedRoute";
function App() {
  const [isAuthenticated,setIsAuthenticated]=useState();
const login=()=>{
setIsAuthenticated(true);
}
const logout=()=>{
  sessionStorage.clear();
  setIsAuthenticated(false);
}
  useEffect(()=>{
    const checkAuth=async()=>{
      await Axios({
        method:'get',
        url:'http://localhost:5000/api/users/isAuthenticated',
        headers: {
          'Authorization': "Bearer "+sessionStorage.getItem('jwt'),
      }
    }).then(res=>{
     if(res.data){
      login();

     } 
    }).catch(err=>{
          logout();
    });

    }
checkAuth();

  },[]);

 

  return (

    <Router>
 <div>
      <Navigation isAuthenticated={isAuthenticated}/>
    </div>
  <Switch>
     
         <Route path="/login" exact component={Login}/>
         <Route path="/register" exact component={Register}/>
         <Route path="/create-link" exact component={CreateLinkPage}  />
         
          
  
     

  
      
      </Switch>
    </Router>

      
  );
}

export default App;
