
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

const FormForgotPasswordRequest = ({onSubmit, error, isLoading}) => {
    const [email, setEmail] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();
        onSubmit(email);
    };

    return (
        <>
            <Form role="form" onSubmit={handleSubmit}>
                <FormGroup className="mb-3">
                    <InputGroup className="input-group-alternative">
                        <InputGroupAddon addonType="prepend">
                            <InputGroupText>
                                <i className="ni ni-email-83" />
                            </InputGroupText>
                        </InputGroupAddon>
                        <Input
                            placeholder="Email"
                            type="email"
                            autoComplete="new-email"
                            required
                            onChange={(e) => setEmail(e.target.value)}
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

export default FormForgotPasswordRequest;
