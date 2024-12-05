import React, { useState } from 'react';
import { Modal, ModalBody, Button } from 'reactstrap';

const ImagePopup = ({ imageSrc, imageAlt = 'Image Preview' }) => {
    const [isOpen, setIsOpen] = useState(false);

    const toggleModal = () => {
        setIsOpen(!isOpen);
    };

    return (
        <div>
            <Button color="primary" onClick={toggleModal} disabled={!imageSrc}>
                {imageSrc ? "View Image" : "No Image"}
            </Button>

            <Modal isOpen={isOpen} toggle={toggleModal} centered>
                <ModalBody>
                    <div className="text-center">
                        {imageSrc
                            ? (
                                <img
                                    src={imageSrc}
                                    alt={imageAlt}
                                    style={{ maxWidth: '100%', maxHeight: '400px' }}
                                />
                            )
                            : (<h3>No Image</h3>)}
                    </div>
                </ModalBody>
            </Modal>
        </div>
    );
};

export default ImagePopup;
