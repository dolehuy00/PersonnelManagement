
import {
    Button,
    Spinner,
    Alert
} from "reactstrap";
import React, { useState } from "react";
import CountdownTimerButton from "components/Button/ButtonCountdownTimer.js";

const FormForgotPasswordInputCode = ({
    onSubmit,
    isLoading,
    error,
    onClickGetCodeAgain,
    lodingGetCodeAgain
}) => {
    const [code, setCode] = useState(new Array(6).fill(""));

    // Hàm xử lý thay đổi
    const handleChange = (e, index) => {
        const value = e.target.value;

        if (!isNaN(value) && value.length <= 1) {
            const newCode = [...code];
            newCode[index] = value;
            setCode(newCode);

            // Tự động chuyển qua ô tiếp theo
            if (value !== "" && index < 5) {
                const nextInput = document.getElementById(`code-input-${index + 1}`);
                if (nextInput) nextInput.focus();
            }
        }
    };

    // Hàm xử lý xóa (Backspace)
    const handleKeyDown = (e, index) => {
        if (e.key === "Backspace" && !code[index] && index > 0) {
            const prevInput = document.getElementById(`code-input-${index - 1}`);
            if (prevInput) prevInput.focus();
        }
    };

    // Hàm xử lý submit
    const handleSubmitForm = (e) => {
        e.preventDefault();
        onSubmit(code.join(""));
    };

    return (
        <>
            <form onSubmit={handleSubmitForm}>
                <div className="d-flex justify-content-center" style={{ gap: "10px" }}>
                    {/* render inputs */}
                    {code.map((value, index) => (
                        <input
                            key={index}
                            id={`code-input-${index}`}
                            type="text"
                            maxLength="1"
                            value={value}
                            required
                            onChange={(e) => handleChange(e, index)}
                            onKeyDown={(e) => handleKeyDown(e, index)}
                            style={{
                                width: "40px",
                                height: "40px",
                                textAlign: "center",
                                fontSize: "18px",
                                border: "1px solid #ccc",
                                borderRadius: "4px",
                            }}
                        />
                    ))}
                </div>
                {/* message error */}
                {error && (
                    <Alert color="danger" className="py-1 mb-0 mt-2">
                        {error}
                    </Alert>
                )}
                {/* button confirm */}
                <div className="text-center">
                    <CountdownTimerButton disableTime={70} onClick={onClickGetCodeAgain} loading={lodingGetCodeAgain} />
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
            </form>
        </>
    );
};

export default FormForgotPasswordInputCode;
