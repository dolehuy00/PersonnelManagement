
// reactstrap components
import {
    Spinner,
} from "reactstrap";
import React, { useState } from 'react';

const ImageWithSkeleton = ({ src, placeholder, alt, className, transform = "translate(-50%, -50%)", showSpiner = false, onClick, timeOut = 30000 }) => {
    const [loaded, setLoaded] = useState(false);
    let [isTimeOut, setIsTimeOut] = useState(false);

    setTimeout(() => {
        setIsTimeOut(true);
    }, timeOut);

    return (
        <>
            <div style={{ position: "relative" }}>
                {!isTimeOut && showSpiner && (!loaded || !src)
                    ? (
                        <div
                            style={{
                                position: "absolute",
                                top: "50%",
                                left: "50%",
                                transform: transform,
                                zIndex: 10,
                            }}
                        >
                            <Spinner color="info" />
                        </div>
                    )
                    : ""
                }
                {src
                    ? (
                        <img
                            className={className}
                            src={src}
                            onLoad={() => { setLoaded(true) }}
                            alt={alt}
                            onClick={onClick}
                        />
                    )
                    : (
                        <img
                            className={className}
                            src={placeholder}
                            alt={alt}
                            onClick={onClick}
                        />
                    )
                }
            </div>
        </>
    );

};

export default ImageWithSkeleton;