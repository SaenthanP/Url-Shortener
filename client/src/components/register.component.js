import { Container, Col, Row, Form, Card, Button } from 'react-bootstrap';
import React, { useState, useEffect } from 'react';
import Axios from 'axios';
import ErrorModal from './error-modal.component';

import './styles/auth.css'
export default function Register() {
    const [username, setUsername] = useState();
    const [password, setPassword] = useState();
    const [confirmPassword, setConfirmPassword] = useState();
    const [error, setError] = useState();
    const [modalShow, setModalShow] = useState(false);




    const onSubmit = async (e) => {
        try {
            e.preventDefault();
            const user = {
                username,
                password,
                confirmPassword
            }
            const userRes = await Axios.post("http://localhost:5000/api/users", user);

        } catch (err) {
        // console.log(err.response.data[0].errors[0].errorMessage);
                setError(err.response.data[0].errors[0].errorMessage);

    
            setModalShow(true);
        }


    }
    return (

        <Container>
            <Row>
                <Col xs={12}>
                </Col>
                <Card className="auth-card">
                    <ErrorModal
                        show={modalShow}
                        onHide={() => setModalShow(false)}
                        error={error}

                    />
                    <Card.Body>
                        <Card.Title>Register</Card.Title>
                        <Form onSubmit={onSubmit}>
                            <Form.Group controlId="formBasicUsername">
                                <Form.Label>Username</Form.Label>
                                <Form.Control type="text" placeholder="Enter username" onChange={(e) => setUsername(e.target.value)} />
                            </Form.Group>
                          
                            <Form.Group controlId="formBasicPassword">
                                <Form.Label>Password</Form.Label>
                                <Form.Control type="password" placeholder="Password" onChange={(e) => setPassword(e.target.value)} />
                            </Form.Group>

                            <Form.Group controlId="formBasicConfirmPassword">
                                <Form.Label>Confirm Password</Form.Label>
                                <Form.Control type="password" placeholder="Re-enter Password" onChange={(e) => setConfirmPassword(e.target.value)} />
                            </Form.Group>
                            <Button variant="primary" type="submit">Register</Button>
                        </Form>

                    </Card.Body>
                </Card>
            </Row>
        </Container>


    );
}