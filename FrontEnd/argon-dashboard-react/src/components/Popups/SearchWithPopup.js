import React, { useState } from 'react';
import {
  Button,
  Input,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
  ListGroup,
  ListGroupItem,
  Spinner,
  Row,
  Col,
} from 'reactstrap';

const SearchWithPopup = ({
  titleModal, // string
  nameInput, // string
  searchApiFunc, // fuction
  propertyInDataToViewSearch, // array dictionaries [{text: "", property: "id"},{text: " ~ ", property: "fullname"}]
  propertyInDataToViewDisableInput, // array ["property_1", "property_2"]
  required, // string
  propertyInDataToSetRealInput,// string
  deboundTimeOut, // integer
  arraySetValueInput, // array
  disabled, // string
  onChange,
  propertyPassedToOtherDataEventOnChange, // array ["property_1", "property_2"]
  departmentId = null// long
}) => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [disabledInputValue, setDisabledInputValue] = useState('');
  const [realInputValue, setRealInputValue] = useState("");
  const [searchValue, setSearchValue] = useState('');
  const [typingTimeout, setTypingTimeout] = useState(null);

  const { data, loading, error, requestSearch } = searchApiFunc();

  // Toggle Popup
  const toggleModal = () => setIsModalOpen(!isModalOpen);

  // Handle search input change with debounce
  const handleSearchChange = (e) => {
    const value = e.target.value;
    setSearchValue(value);

    // Clear previous timeout
    if (typingTimeout) {
      clearTimeout(typingTimeout);
    }

    // Set new timeout
    setTypingTimeout(
      setTimeout(() => {
        requestSearch(value, departmentId);
      }, deboundTimeOut)
    );
  };

  // Handle selection of a search result
  const handleSelectResult = (result) => {
    setDisabledInputValue(
      `${result[propertyInDataToViewDisableInput[0]]}` +
      `${result[propertyInDataToViewDisableInput[1]] ? " ~ " + result[propertyInDataToViewDisableInput[1]] : ""}`);
    setRealInputValue(result[propertyInDataToSetRealInput]);
    let otherData = {};
    if (propertyPassedToOtherDataEventOnChange) {
      propertyPassedToOtherDataEventOnChange.forEach(element => {
        otherData[element] = result[element];
      });
    }
    if (onChange) {
      const fakeEvent = {
        target: {
          name: nameInput,
          value: result[propertyInDataToSetRealInput],
        },
        otherData
      };
      onChange(fakeEvent)
    }
    toggleModal(); // Close modal
  };

  const handleSetData = (result) => {
    setDisabledInputValue(
      `${result[propertyInDataToViewDisableInput[0]]}` +
      `${result[propertyInDataToViewDisableInput[1]] ? " ~ " + result[propertyInDataToViewDisableInput[1]] : ""}`
    ); // Update the disabled input
    setRealInputValue(result[propertyInDataToSetRealInput])
  };

  return (
    <div className="container">
      <Row>
        <Col className="px-0">
          {/* Disabled Input */}
          <Input
            innerRef={() => { if (arraySetValueInput) arraySetValueInput[0] = handleSetData }}
            type="text"
            value={disabledInputValue}
            placeholder="Search and select at the button next to"
            required={required}
            disabled={disabled}
            onChange={(e) => e.preventDefault()}
          />
          <Input
              type="number"
              value={realInputValue}
              hidden
              readOnly
              name={realInputValue?.toString().length > 0 ? nameInput : ""}
              required={required}
            />
        </Col>
        <Col lg={{ size: "auto" }} className="pr-0">
          {/* Button to open modal */}
          <Button color="primary" className="" onClick={toggleModal} disabled={disabled}>
            Choose
          </Button>
        </Col>
      </Row>
      {/* Modal */}
      <Modal isOpen={isModalOpen} toggle={toggleModal}>
        <ModalHeader toggle={toggleModal}>{titleModal}</ModalHeader>
        <ModalBody className="pt-0">
          {/* Input for search */}
          <Input
            type="text"
            placeholder="Type to search..."
            value={searchValue}
            onChange={handleSearchChange}
          />

          {/* Loading spinner */}
          {loading && (
            <div className="text-center mt-3">
              <Spinner color="info" type="grow" size="sm" />
              <p className="mb-0">Loading...</p>
            </div>
          )}

          {/* Display search results */}
          {!loading && (
            <ListGroup className="mt-3">
              {data.map((result) => (
                <ListGroupItem
                  key={result.id}
                  tag="button"
                  action
                  onClick={() => handleSelectResult(result)}
                >
                  {propertyInDataToViewSearch?.map((item) => (item.text + result[item.property]))}
                </ListGroupItem>
              ))}

              {/* No results message */}
              {data?.length === 0 && (
                <p className="text-center mt-3 text-muted">No results found.</p>
              )}
              {/* Error message */}
              {error && (
                <p className="text-center mt-3 text-muted">Error load data.</p>
              )}
            </ListGroup>
          )}
        </ModalBody>
        <ModalFooter>
          <Button color="secondary" onClick={toggleModal}>
            Close
          </Button>
        </ModalFooter>
      </Modal>
    </div>
  );
};

export default SearchWithPopup;
