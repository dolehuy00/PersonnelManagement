// reactstrap components
import {
    Card,
    Container,
    Row,
    Spinner,
    Alert,
    CardBody
} from "reactstrap";

const LoadingOrError = ({ status, errorMessage="Error load data!" }) => {
    return (
        <>
            <Container className="mt--7" fluid>
                <Card className="bg-secondary shadow">
                    <CardBody className="border-0">
                        <Row className="justify-content-center">
                            {status === "loading"
                                ? (<>
                                    <Spinner
                                        className="mx-1"
                                        color="info"
                                        type="grow"
                                    >
                                        Loading...
                                    </Spinner>
                                    <Spinner
                                        className="mx-1"
                                        color="info"
                                        type="grow"
                                    >
                                        Loading...
                                    </Spinner>
                                    <Spinner
                                        className="mx-1"
                                        color="info"
                                        type="grow"
                                    >
                                        Loading...
                                    </Spinner>
                                </>)
                                : (
                                    <Alert className="w-100 text-center mb-0" color="danger">
                                        <i className="fa-solid fa-circle-exclamation mr-1"></i><strong>{errorMessage}</strong>
                                    </Alert>
                                )
                            }
                        </Row>
                    </CardBody>
                </Card>
            </Container>
        </>
    );
}
export default LoadingOrError;