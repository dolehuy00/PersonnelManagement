import React from "react";
// reactstrap components
import {
    Button,
    Form,
    Input,
    Modal,
    Row,
    Col,
    Container
} from "reactstrap";
import DatePickerWithTooltip from "components/DateTimePickers/DatePickerWithTooltip.js";

class Modals extends React.Component {
    state = {
        exampleModal: false,
        dataFilters: {},
        isValid: true
    };

    constructor(props) {
        super(props);
        this.datePickerRefs = {};
    }

    toggleModal = state => {
        this.setState({
            [state]: !this.state[state]
        });
    };

    handleInputChange = (event) => {
        const { name, value } = event.target;
        this.setState(prevState => ({
            dataFilters: {
                ...prevState.dataFilters,
                [name]: value
            }
        }));
    };

    handleDateChange = (date, isValid, name) => {
        this.setState(prevState => ({
            isValid: date === "" ? true : isValid,
            dataFilters: {
                ...prevState.dataFilters,
                [name]: date
            }
        }));
    };

    handleClear = () => {
        console.log(this.datePickerRefs);
        Object.keys(this.datePickerRefs).forEach((key) => {
            this.datePickerRefs[key].current.state.inputValue = '';
        });
        this.setState({ dataFilters: {} });
    }

    render() {
        // itemSingleFilters = [{ labelName: "Name Or Id", nameInput: "nameOrId", type: "text" }];
        // itemRangeFilters = [{ labelName: "Salary", nameInputFrom: "fromSalary", nameInputTo: "toSalary", type: "number" }];
        // itemSelectOptions = [{ labelName: "Status", nameSelect: "status", Option[{labelName: "Lock", value: "Lock"}, {labelName: "Active", value: "Active"}], type: "select_option"}]

        const { itemSingleFilters, itemRangeFilters, itemSelectOptions, onConfirmFilter, dataFilterUseState } = this.props;

        if (itemRangeFilters) {
            itemRangeFilters.forEach((item) => {
                if (item.type === "date") {
                    this.datePickerRefs[`datePicker${item.nameInputFrom}`] = React.createRef();
                    this.datePickerRefs[`datePicker${item.nameInputTo}`] = React.createRef();
                }
            });
        }

        const handleOpen = () => {
            Object.entries(dataFilterUseState).forEach(([key, value]) => {
                this.setState(prevState => ({
                    dataFilters: {
                        ...prevState.dataFilters,
                        [key]: value
                    }
                }));
            });
        };

        const handleConfirm = (event) => {
            event.preventDefault();
            onConfirmFilter(this.state["dataFilters"]);
            this.toggleModal("exampleModal");
        };

        return (
            <>
                {/* Button trigger modal */}
                <Button
                    color="primary"
                    type="button"
                    outline
                    size="sm"
                    onClick={() => {
                        this.toggleModal("exampleModal");
                        handleOpen()
                    }}>
                    <i className="fas fa-filter"></i>
                    <span>
                        ({Object.values(dataFilterUseState).filter(value => value !== "").length})
                    </span>
                </Button>
                {/* Modal */}
                <Modal
                    className="modal-dialog-centered  modal-lg"
                    isOpen={this.state.exampleModal}
                    toggle={() => this.toggleModal("exampleModal")}
                >
                    <div className="modal-header d-flex align-items-center">
                        <h3 className="modal-title" id="exampleModalLabel">
                            Filter
                        </h3>
                        <button
                            aria-label="Close"
                            className="close"
                            data-dismiss="modal"
                            type="button"
                            onClick={() => this.toggleModal("exampleModal")}
                        >
                            <span aria-hidden={true}>×</span>
                        </button>
                    </div>
                    <Form onSubmit={handleConfirm}>
                        <div className="modal-body pt-0">
                            <Container>
                                {itemSingleFilters && (<h3 className="text-center mt-0">Single Filter</h3>)}
                                {itemSingleFilters
                                    ? itemSingleFilters.map((item) => (
                                        <Row
                                            className="d-flex align-items-center mb-2"
                                            key={"filter-input-" + item.nameInput}
                                        >
                                            <Col lg="3">
                                                <span>{item.labelName}</span>
                                            </Col>
                                            <Col>
                                                <Input
                                                    className="form-control-alternative text-body"
                                                    id="exampleFormControlInput1"
                                                    type={item.type}
                                                    name={item.nameInput}
                                                    value={this.state.dataFilters[item.nameInput] || ""}
                                                    onChange={this.handleInputChange}
                                                />
                                            </Col>
                                        </Row>))
                                    : ""}
                                {
                                    itemSelectOptions &&
                                    itemSelectOptions.map((item) => (
                                        <Row className="d-flex align-items-center mb-2" key={"filter-select-" + item.nameSelect}>
                                            <Col lg="3">
                                                <span>{item.labelName}</span>
                                            </Col>
                                            <Col>
                                                <Input
                                                    type="select"
                                                    name={item.nameSelect}
                                                    className="form-control-alternative"
                                                    value={this.state.dataFilters[item.nameSelect] || ""}
                                                    onChange={this.handleInputChange}
                                                >
                                                    <option value="">-- Select --</option>
                                                    {item.Option.map((option) => (
                                                        <option key={option.value} value={option.value}>
                                                            {option.labelName}
                                                        </option>
                                                    ))}
                                                </Input>
                                            </Col>
                                        </Row>
                                    ))
                                }
                                {itemRangeFilters && (<h3 className="text-center mt-4">Range Filter</h3>)}
                                {itemRangeFilters
                                    ? itemRangeFilters.map((item) => (
                                        item.type === "date"
                                            ? (
                                                <Row
                                                    className="d-flex align-items-center mb-2"
                                                    key={"filter-input-" + item.nameInputFrom}
                                                >
                                                    <Col lg="3">
                                                        <span>{item.labelName}</span>
                                                    </Col>
                                                    <Col>
                                                        <div className="input-group-alternative rounded-lg">
                                                            <DatePickerWithTooltip
                                                                ref={this.datePickerRefs[`datePicker${item.nameInputFrom}`]}
                                                                value={this.state.dataFilters[item.nameInputFrom] || ""}
                                                                dateFormat="YYYY-MM-DD"
                                                                className="form-control-alternative text-body form-control"
                                                                name={item.nameInputFrom}
                                                                placeholder="YYYY-MM-DD"
                                                                id={`datePicker` + item.nameInputFrom}
                                                                onChange={(date, isValid) => this.handleDateChange(date, isValid, item.nameInputFrom)}
                                                            />
                                                        </div>
                                                    </Col>
                                                    <span>-</span>
                                                    <Col>
                                                        <div className="input-group-alternative rounded-lg">
                                                            <DatePickerWithTooltip
                                                                ref={this.datePickerRefs[`datePicker${item.nameInputTo}`]}
                                                                value={this.state.dataFilters[item.nameInputTo] || ""}
                                                                dateFormat="YYYY-MM-DD"
                                                                className="form-control-alternative text-body form-control"
                                                                name={item.nameInputTo}
                                                                placeholder="YYYY-MM-DD"
                                                                id={`datePicker` + item.nameInputTo}
                                                                onChange={(date, isValid) => this.handleDateChange(date, isValid, item.nameInputTo)}
                                                            />
                                                        </div>
                                                    </Col>
                                                </Row>
                                            )
                                            : (
                                                <Row
                                                    className="d-flex align-items-center mb-2"
                                                    key={"filter-input-" + item.nameInputFrom}
                                                >
                                                    <Col lg="3">
                                                        <span>{item.labelName}</span>
                                                    </Col>
                                                    <Col>
                                                        <Input
                                                            className="form-control-alternative"
                                                            id="exampleFormControlInput1"
                                                            type={item.type}
                                                            name={item.nameInputFrom}
                                                            value={this.state.dataFilters[item.nameInputFrom] || ""}
                                                            onChange={this.handleInputChange}
                                                        />
                                                    </Col>
                                                    <span>-</span>
                                                    <Col>
                                                        <Input
                                                            className="form-control-alternative"
                                                            id="exampleFormControlInput1"
                                                            type={item.type}
                                                            name={item.nameInputTo}
                                                            value={this.state.dataFilters[item.nameInputTo] || ""}
                                                            onChange={this.handleInputChange}
                                                        />
                                                    </Col>
                                                </Row>
                                            )
                                    ))
                                    : ""
                                }                                
                            </Container>
                        </div>
                        <div className="modal-footer">
                            <Button
                                color="secondary"
                                data-dismiss="modal"
                                type="button"
                                onClick={this.handleClear}
                            >
                                Clear
                            </Button>
                            <Button disabled={!this.state.isValid} color="primary" type="submit">
                                Comfirm
                            </Button>
                        </div>
                    </Form>
                </Modal>
            </>
        );
    }
}

export default Modals;
