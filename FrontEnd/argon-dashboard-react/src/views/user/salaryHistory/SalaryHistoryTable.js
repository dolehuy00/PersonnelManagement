
// reactstrap components
import {
  Card,
  CardHeader,
  CardFooter,
  Table,
  Container,
  Row,
  Col,
} from "reactstrap";

// core components
import React, { useState } from "react";

// hooks
import { useFilterSalaryHistoryByUser } from "hooks/UseSalaryHistoryApi.js";

// components
import CustomPagination from "components/Pagination/Pagination.js";
import DropdownButtonSmall from "components/Dropdowns/DropdownButtonSmall.js";
import Header from "components/Headers/Header.js";
import LoadingOrError from "components/Notifications/LoadingOrError.js";
import { useNavigate } from "react-router-dom";

const Tables = () => {
  // data component
  const arrayItemPerPage = [
    { text: "Item per page: ", value: 5 },
    { text: "Item per page: ", value: 10 },
    { text: "Item per page: ", value: 30 },
    { text: "Item per page: ", value: 50 },
    { text: "Item per page: ", value: 100 }
  ];
  const arrayItemSortBy = [
    { text: "No sort", value: "" },
    { text: "Sort by date ascending", value: "asc" },
    { text: "Sort by date decreasing", value: "dec" },
  ];

  //state constant
  const navigate = useNavigate();
  const [pageIndex, setPageIndex] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [sortBy, setSortBy] = useState("");
  const buttonPerPageState = useState(arrayItemPerPage[0]);
  const buttonSortByState = useState(arrayItemSortBy[0])

  // handle function
  const handlePageChange = (newPageIndex) => {
    setPageIndex(newPageIndex);
  };

  const handleSelectPerPageChange = (newItemPerPage) => {
    setPageIndex(1);
    setPageSize(newItemPerPage);
  };

  const handleSelectSortByChange = (newItem) => {
    setSortBy(newItem);
  }

  // request data
  const { data, loading, error } = useFilterSalaryHistoryByUser(sortBy, pageIndex, pageSize);
  const totalPage = data.totalPage;

  // render
  if (loading) return (
    <>
      <Header />
      <LoadingOrError status="loading" />
    </>
  );

  if (error) return (
    <>
      <Header />
      <LoadingOrError status="error" />
    </>
  );

  return (
    <>
      <Header />
      {/* Page content */}
      <Container className="mt--7" fluid>
        {/* Table */}
        <Row>
          <div className="col">
            <Card className="shadow">
              <CardHeader className="border-0">
                <Container className="mw-100">
                  <Row>
                    <Col className="text-right">
                      <DropdownButtonSmall
                        selectedItemState={buttonSortByState}
                        viewValue={false}
                        arrayItems={arrayItemSortBy}
                        onSelectChange={handleSelectSortByChange}
                      />
                    </Col>
                  </Row>
                </Container>
              </CardHeader>
              <Table className="align-items-center table-flush" responsive>
                <thead className="thead-light">
                  <tr className="text-center">
                    <th scope="col">Basic Salary</th>
                    <th scope="col">Bonus Salary</th>
                    <th scope="col">Penalty</th>
                    <th scope="col">Tax</th>
                    <th scope="col">Date<br />(YYYY-MM-DD)</th>
                  </tr>
                </thead>
                <tbody>
                  {data
                    ? data.results.map((item, index) => (
                      <tr
                        key={index}
                        className="text-center table-row-salary-user"
                        onClick={(e) => { e.preventDefault(); navigate(`/salary-history/view?id=${item.id}`); }}
                      >
                        <td>{item.basicSalary}</td>
                        <td>{item.bonusSalary}</td>
                        <td>
                          {item.penalty}
                        </td>
                        <td>{item.tax}</td>
                        <td>{item.date.split("T")[0]}</td>
                      </tr>
                    ))
                    : ""}
                </tbody>
              </Table>
              <CardFooter className="py-4">
                <Container className="mw-100">
                  <Row>
                    <Col>
                      <DropdownButtonSmall selectedItemState={buttonPerPageState} viewValue={true} arrayItems={arrayItemPerPage} onSelectChange={handleSelectPerPageChange} />
                    </Col>
                    <Col>
                      <CustomPagination pageIndex={pageIndex} totalPage={totalPage} maxPageView={5} onPageChange={handlePageChange} />
                    </Col>
                  </Row>
                </Container>
              </CardFooter>
            </Card>
          </div>
        </Row>
      </Container>
    </>
  );
};

export default Tables;
