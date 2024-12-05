
// reactstrap components
import {
    Button,
    FormGroup,
    Form,
    Input,
    InputGroupAddon,
    InputGroupText,
    InputGroup,
    Alert,
    Spinner
} from "reactstrap";

import React, { useState } from "react";

const FormForgotPasswordChangePassword = ({ onSubmit, error, isLoading }) => {
    // state variable
    const [password, setPassword] = useState("");
    const [confirmPassword, setComfirmPassword] = useState("");

    // handle submit form
    const handleSubmit = async (e) => {
        e.preventDefault();
        onSubmit(password, confirmPassword);
    };

    return (
        <>
            <Form role="form" onSubmit={handleSubmit}>
                <FormGroup className="mb-2">
                    <InputGroup className="input-group-alternative">
                        <InputGroupAddon addonType="prepend">
                            <InputGroupText>
                                <i className="ni ni-lock-circle-open" />
                            </InputGroupText>
                        </InputGroupAddon>
                        <Input
                            placeholder="Password"
                            type="password"
                            autoComplete="new-password"
                            required
                            onChange={(e) => setPassword(e.target.value)}
                        />
                    </InputGroup>
                </FormGroup>
                <FormGroup className="mb-2">
                    <InputGroup className="input-group-alternative">
                        <InputGroupAddon addonType="prepend">
                            <InputGroupText>
                                <i className="fa-solid fa-check-double"></i>
                            </InputGroupText>
                        </InputGroupAddon>
                        <Input
                            placeholder="Confirm Password"
                            type="password"
                            autoComplete="new-password"
                            required
                            onChange={(e) => setComfirmPassword(e.target.value)}
                        />
                    </InputGroup>
                </FormGroup>
                {error && (
                    <Alert color="danger" className="py-1 mb-0">
                        {error}
                    </Alert>
                )}
                <div className="text-center">
                    {isLoading ? (
                        <Button className="mt-2" color="primary" type="button" disabled>
                            <Spinner size="sm">
                                Wating...
                            </Spinner>
                            <span>
                                {' '}Wating...
                            </span>
                        </Button>
                    ) : (
                        <Button className="mt-2" color="primary" type="submit">
                            Confirm
                        </Button>
                    )}
                </div>
            </Form>
        </>
    );
};

export default FormForgotPasswordChangePassword;
