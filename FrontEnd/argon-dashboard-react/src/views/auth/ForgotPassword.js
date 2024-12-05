
// reactstrap components
import {
    Card,
    CardHeader,
    CardBody,
    Row,
    Col,
    Alert,
} from "reactstrap";

import React, { useState } from "react";
import {
    useForgotPassword,
    useForgotPasswordVertifyCode,
    useForgotPasswordChangePassword
} from "hooks/UseAuthApi";
import FormForgotPasswordInputCode from "components/Form/FormForgotPasswordInputCode.js";
import FormForgotPasswordRequest from "components/Form/FormForgotPasswordRequest.js";
import FormForgotPasswordChangePassword from "components/Form/FormForgotPasswordChangePassword.js";

const ForgotRequest = () => {
    // state variable
    const [email, setEmail] = useState("");
    const [code, setCode] = useState(0);
    const [forgotStep, setForgotStep] = useState(1);
    const [messageSuccess, setMessageSuccess] = useState('');

    // request
    const {
        handleForgot,
        isLoading: loadingRequest,
        error: errorRequest
    } = useForgotPassword();
    const {
        handleForgotPasswordVertifyCode,
        isLoading: loadingVetify,
        error: errorVetify
    } = useForgotPasswordVertifyCode();
    const {
        handleForgotPasswordChangePassword,
        isLoading: loadingChange,
        error: errorChange
    } = useForgotPasswordChangePassword();

    // handle function
    const onSubmitRequestForgot = async (email) => {
        const data = await handleForgot(email);
        if (data) {
            setEmail(email);
            setForgotStep(2);
            setMessageSuccess("Check your email for the code.");
        }
    };

    const onSubmitVetify = async (code) => {
        const data = await handleForgotPasswordVertifyCode(email, code);
        if (data) {
            setCode(code);
            setForgotStep(3);
            setMessageSuccess("Code vetified, confirm new your password.");
        }
    };

    const onSubmitChange = async (password, confirmPassword) => {
        const data = await handleForgotPasswordChangePassword(email, code, password, confirmPassword);
        if (data) {
            setForgotStep(4);
            setCode(0);
            setEmail("");
            setMessageSuccess("");
        }
    };

    const handleClickGetCodeAgain = async () => {
        const data = await handleForgot(email);
        if (data) {
            setForgotStep(2);
            setMessageSuccess("Check your email for the code.");
        } else {
            setForgotStep(1);
            setMessageSuccess("");
        }
    }

    return (
        <>
            <Col lg="5" md="7">
                <Card className="bg-secondary shadow border-0">
                    <CardHeader className="bg-transparent">
                        <div className="text-muted text-center m-0">
                            <h4 className="m-0">
                                Forgot Password {forgotStep === 2 ? "(Vetify Code)" : forgotStep === 3 ? "(Change Password)" : ""}
                            </h4>
                        </div>
                    </CardHeader>
                    <CardBody className="px-lg-5 py-4">
                        {messageSuccess !== ''
                            ? (
                                <div className="d-flex justify-content-center">
                                    <Alert className="p-1 w-100 text-center" color="primary">
                                        {messageSuccess}
                                    </Alert>
                                </div>
                            )
                            : ""
                        }
                        {forgotStep === 1
                            ? (
                                <FormForgotPasswordRequest
                                    onSubmit={onSubmitRequestForgot}
                                    error={errorRequest}
                                    isLoading={loadingRequest}
                                />
                            )
                            : forgotStep === 2
                                ? (
                                    <FormForgotPasswordInputCode
                                        onSubmit={onSubmitVetify}
                                        error={errorVetify}
                                        isLoading={loadingVetify}
                                        lodingGetCodeAgain={loadingRequest}
                                        onClickGetCodeAgain={handleClickGetCodeAgain}
                                    />
                                )
                                : forgotStep === 3
                                    ? (
                                        <FormForgotPasswordChangePassword
                                            onSubmit={onSubmitChange}
                                            error={errorChange}
                                            isLoading={loadingChange}
                                        />
                                    )
                                    : forgotStep === 4
                                        ? (
                                            <div className="d-flex justify-content-center">
                                                <Alert>
                                                    Success! You can login now.
                                                </Alert>
                                            </div>
                                        )
                                        : (
                                            <p className="text-center" style={{ color: "red" }}>
                                                <b>Any error, please refresh page!</b>
                                            </p>
                                        )
                        }
                    </CardBody>
                </Card>
                <Row className="mt-3">
                    <Col xs="6">
                        <a
                            className="text-light"
                            href="login"
                        >
                            <small>Back to sign in</small>
                        </a>
                    </Col>
                </Row>
            </Col>
        </>
    );
};

export default ForgotRequest;
