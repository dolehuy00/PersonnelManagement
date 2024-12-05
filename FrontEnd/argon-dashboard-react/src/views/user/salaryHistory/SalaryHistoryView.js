// reactstrap components
import {
    Card,
    CardHeader,
    Container,
    Row,
    Col,
    Input,
    CardBody,
    FormGroup,
    Form,
} from "reactstrap";

// core components
import { useLocation } from 'react-router-dom';

// component
import Header from "components/Headers/Header.js";
import LoadingOrError from "components/Notifications/LoadingOrError.js";

// custom hooks
import {useGetOneSalaryHistoryByUser} from "hooks/UseSalaryHistoryApi.js";

const ViewSalaryHistory = () => {
    // search params
    const location = useLocation();
    const searchParams = new URLSearchParams(location.search);
    const salaryHistoryId = searchParams.get("id");

    // request data
    const { data: dataGetSalary, loading: loadingGetSalary, error: errorGetSalary } = useGetOneSalaryHistoryByUser(salaryHistoryId);

    // render loading data
    if (loadingGetSalary) return (
        <>
            <Header />
            <LoadingOrError status="loading" />
        </>
    );

    // render error load data
    if (errorGetSalary) return (
        <>
            <Header />
            <LoadingOrError status="error" errorMessage="Can't load salary !"/>
        </>
    );

    // render data
    return (
        <>
            <Header />
            {/* Page content */}
            <Container className="mt--7" fluid>
                <Row>
                    <div className="col">
                        <Card className="bg-secondary shadow">
                            <CardHeader className="bg-white border-0">
                                <Row className="align-items-center">
                                    <Col xs="8">
                                        <h3 className="mb-0">
                                            Salary Details
                                        </h3>
                                    </Col>
                                </Row>
                            </CardHeader>
                            <CardBody>
                                {dataGetSalary.results
                                    ? (<>
                                        <Form>
                                            <div className="pl-lg-4">
                                                <Row>
                                                    <Col lg="2">
                                                        <FormGroup>
                                                            <label
                                                                className="form-control-label"
                                                                htmlFor="input-id"
                                                            >
                                                                Salary ID
                                                            </label>
                                                            <Input
                                                                className="form-control-alternative"
                                                                id="input-id"
                                                                type="text"
                                                                name="id"
                                                                value={dataGetSalary.results[0]?.id || ""}
                                                                required
                                                                disabled
                                                            />
                                                        </FormGroup>
                                                    </Col>
                                                </Row>
                                                <Row>
                                                    <Col lg="6">
                                                        <FormGroup>
                                                            <label
                                                                className="form-control-label"
                                                                htmlFor="input-basic-salary"
                                                            >
                                                                Basic Salary
                                                            </label>
                                                            <Input
                                                                className="form-control-alternative"
                                                                id="input-basic-salary"
                                                                type="number"
                                                                step="0.01"
                                                                name="basicSalary"
                                                                value={dataGetSalary.results[0]?.basicSalary || ""}
                                                                onChange={(e) => e.preventDefault()}
                                                                required
                                                            />
                                                        </FormGroup>
                                                    </Col>
                                                    <Col lg="6">
                                                        <FormGroup>
                                                            <label
                                                                className="form-control-label"
                                                                htmlFor="input-bonus-salary"
                                                            >
                                                                Bonus Salary
                                                            </label>
                                                            <Input
                                                                className="form-control-alternative"
                                                                id="input-bonus-salary"
                                                                type="number"
                                                                step="0.01"
                                                                value={dataGetSalary.results[0]?.bonusSalary || ""}
                                                                name="bonusSalary"
                                                                onChange={(e) => e.preventDefault()}
                                                                required
                                                            />
                                                        </FormGroup>
                                                    </Col>
                                                </Row>
                                                <Row>
                                                    <Col lg="6">
                                                        <FormGroup>
                                                            <label
                                                                className="form-control-label"
                                                                htmlFor="input-penalty-salary"
                                                            >
                                                                Penalty
                                                            </label>
                                                            <Input
                                                                className="form-control-alternative"
                                                                id="input-penalty-salary"
                                                                type="number"
                                                                step="0.01"
                                                                name="penalty"
                                                                value={dataGetSalary.results[0]?.penalty || ""}
                                                                onChange={(e) => e.preventDefault()}
                                                                required
                                                            />
                                                        </FormGroup>
                                                    </Col>
                                                    <Col lg="6">
                                                        <FormGroup>
                                                            <label
                                                                className="form-control-label"
                                                                htmlFor="input-tax-salary"
                                                            >
                                                                Tax
                                                            </label>
                                                            <Input
                                                                className="form-control-alternative"
                                                                id="input-tax-salary"
                                                                type="number"
                                                                step="0.01"
                                                                name="tax"
                                                                value={dataGetSalary.results[0]?.tax || ""}
                                                                onChange={(e) => e.preventDefault()}
                                                                required
                                                            />
                                                        </FormGroup>
                                                    </Col>
                                                </Row>
                                                <Row>
                                                    <Col lg="6">
                                                        <FormGroup>
                                                            <label
                                                                className="form-control-label"
                                                                htmlFor="input-date-of-birth"
                                                            >
                                                                Date
                                                            </label>
                                                            <Input
                                                                className="form-control-alternative"
                                                                id="input-tax-salary"
                                                                type="text"
                                                                name="date"
                                                                value={dataGetSalary.results[0]?.date || ""}
                                                                onChange={(e) => e.preventDefault()}
                                                                required
                                                            />
                                                        </FormGroup>
                                                    </Col>
                                                </Row>
                                            </div>
                                            <hr className="my-3" />
                                            <div className="pl-lg-4">
                                                <Row>
                                                    <Col>
                                                        <FormGroup>
                                                            <label
                                                                className="form-control-label"
                                                                htmlFor="input-tax-salary"
                                                            >
                                                                Detail
                                                            </label>
                                                            <Input
                                                                className="form-control-alternative"
                                                                id="input-tax-salary"
                                                                rows="3"
                                                                type="textarea"
                                                                name="detail"
                                                                value={dataGetSalary.results[0]?.detail || ""}
                                                                onChange={(e) => e.preventDefault()}
                                                                required
                                                            />
                                                        </FormGroup>
                                                    </Col>
                                                </Row>
                                            </div>
                                            <hr />
                                            <div className="mx-4 py-0 text-right">
                                                <h2><b>Total Salary:</b> {dataGetSalary.results[0]?.total}</h2>
                                            </div>
                                        </Form>
                                    </>)
                                    : ""}
                            </CardBody>
                        </Card>
                    </div>
                </Row>
            </Container>
        </>
    );
};

export default ViewSalaryHistory;