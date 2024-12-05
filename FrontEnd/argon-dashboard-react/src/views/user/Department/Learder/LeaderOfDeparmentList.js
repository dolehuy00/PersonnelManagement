import {
    Card,
    CardBody,
    Container,
    Row,
    Col,
    CardTitle
} from "reactstrap";

import Header from "components/Headers/Header.js";
import { useNavigate } from "react-router-dom";

const LeaderOfDeparmentList = (props) => {
    const leaderOfDepartment = JSON.parse(localStorage.getItem("leaderOfDepartments"));
    const navigate = useNavigate();

    return (
        <>
            <Header />
            <div className="header bg-gradient-info pb-9 pt-0">
                <Container fluid style={{ "marginTop": "-10rem" }}>
                    <div className="header-body">
                        {/* Card stats */}
                        <Row>
                            {leaderOfDepartment?.length > 0
                                ? leaderOfDepartment.map(item => (
                                    <Col key={`deparment-${item.id}`} lg="6" xl="3">
                                        <Card 
                                        className="card-stats mb-4 mb-xl-0 card-department-management"
                                        onClick={e => {navigate(`/leader/dept-assignment?department=${item.id}`)}}
                                        >
                                            <CardBody>
                                                <Row>
                                                    <div className="col">
                                                        <CardTitle
                                                            tag="h5"
                                                            className="text-uppercase text-muted mb-0"
                                                        >
                                                            Department
                                                        </CardTitle>
                                                        <span className="h2 font-weight-bold mb-0">
                                                            {item.name}
                                                        </span>
                                                    </div>
                                                    <Col className="col-auto">
                                                        <div className="icon icon-shape bg-danger text-white rounded-circle shadow">
                                                            <i className="fa-solid fa-building-user" />
                                                        </div>
                                                    </Col>
                                                </Row>
                                                <p className="mt-3 mb-4 text-muted text-sm"></p>
                                            </CardBody>
                                        </Card>
                                    </Col>
                                ))

                                : ""}
                        </Row>
                    </div>
                </Container>
            </div>
        </>
    );
};

export default LeaderOfDeparmentList;
