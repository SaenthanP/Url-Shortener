import { useEffect } from "react";
import { useHistory } from "react-router-dom";
import Axios from 'axios';

export default function CreateLinkPage(){
    const history = useHistory();


    useEffect(()=>{
        const checkAuth=async()=>{
            await Axios({
              method:'get',
              url:'http://localhost:5000/api/users/isAuthenticated',
              headers: {
                'Authorization': "Bearer "+sessionStorage.getItem('jwt'),
            }
          }).then(res=>{
          
          }).catch(err=>{   
            sessionStorage.clear();
            window.location="/login";
        });
      
          }
      checkAuth();
      },[]);

    return (<p>r</p>)
}