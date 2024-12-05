import {
    Button
} from "reactstrap";
import React, { useState, useEffect } from "react";

const CountdownTimerButton = ({ disableTime, onClick, loading }) => {
    const [seconds, setSeconds] = useState(disableTime);

    const handleClick = (event) => {
        event.preventDefault();
        setSeconds(disableTime);
        onClick();
    }

    useEffect(() => {
        if (seconds > 0) {
            const timer = setInterval(() => {
                setSeconds((prevSeconds) => prevSeconds - 1);
            }, 1000);

            return () => clearInterval(timer);
        }
    }, [seconds]);

    return (
        <>
            {loading
                ? (
                    <Button className="mt-2" color="primary" type="button" disabled>
                        Wating...
                    </Button>
                )
                : seconds > 0
                    ? (
                        <Button className="mt-2" color="primary" type="button" disabled>
                            Get code {`(${seconds})`}
                        </Button>
                    )
                    : (
                        <Button className="mt-2" color="primary" type="button" onClick={handleClick}>
                            Get code again
                        </Button>
                    )
            }
        </>
    );
};

export default CountdownTimerButton;
