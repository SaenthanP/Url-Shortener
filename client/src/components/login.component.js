import { Container, Col, Row, Form, Card, Button } from 'react-bootstrap';
import React, { useState, useEffect } from 'react';
import Axios from 'axios';
import ErrorModal from './error-modal.component';
import Cookies from 'js-cookie';
import './styles/auth.css';
import { useHistory } from "react-router-dom";

export default function Login() {
    const history = useHistory();

    const [username, setUsername] = useState();
    const [password, setPassword] = useState();
    const [error, setError] = useState();
    const [modalShow, setModalShow] = useState(false);

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
        window.location="/create-link";
  
       } 
      }).catch(err=>{
        sessionStorage.clear();

    });
  
      }
  checkAuth();
},[]);

    const onSubmit = async (e) => {
        try {
            e.preventDefault();
            const user = {
                username,
                password
            }
            const userRes = await Axios.post("http://localhost:5000/api/users/authenticate", user);
        sessionStorage.setItem('jwt',userRes.data.token);
       
        window.location="/create-link";
        } catch (err) {
                setError(err.response.data[0].errors[0].errorMessage);

            setModalShow(true);
        }


    }
    return (

        <Container>
            <Row>
                <Col xs={6} md={12}>
                </Col>
                <Card className="auth-card">
                    <ErrorModal
                        show={modalShow}
                        onHide={() => setModalShow(false)}
                        error={error}

                    />
                    <Card.Body>
                        <Card.Title>Login</Card.Title>
                        <Form onSubmit={onSubmit}>
                            <Form.Group controlId="formBasicUsername">
                                <Form.Label>Username</Form.Label>
                                <Form.Control type="text" placeholder="Enter username" onChange={(e) => setUsername(e.target.value)} />
                            </Form.Group>
                            <Form.Group controlId="formBasicPassword">
                                <Form.Label>Password</Form.Label>
                                <Form.Control type="password" placeholder="Password" onChange={(e) => setPassword(e.target.value)} />
                            </Form.Group>

                            <Button variant="primary" type="submit">Login</Button>
                        </Form>

                    </Card.Body>
                </Card>
            </Row>
        </Container>


    );
}