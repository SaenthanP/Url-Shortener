import logo from './logo.svg';
import { BrowserRouter as Router, Route ,Switch,Redirect} from "react-router-dom";
import Login from "./components/login.component";
import Register from "./components/register.component";
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';

function App() {
  return (
    <Router>
      <Switch>
        <Route path="/login" exact component={Login}/>
        <Route path="/register" exact component={Register}/>

      </Switch>
    </Router>
  );
}

export default App;
