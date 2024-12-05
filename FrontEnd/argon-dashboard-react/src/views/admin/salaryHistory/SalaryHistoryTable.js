
// reactstrap components
import {
    Card,
    CardHeader,
    CardFooter,
    DropdownMenu,
    DropdownItem,
    UncontrolledDropdown,
    DropdownToggle,
    Table,
    Container,
    Row,
    Col,
    Button,
  } from "reactstrap";

  // core components
  import React, { useState } from "react";

  // hooks
  import { useFilterSalaryHistory } from "hooks/UseSalaryHistoryApi.js";

  // components
  import CustomPagination from "components/Pagination/Pagination.js";
  import DropdownButtonSmall from "components/Dropdowns/DropdownButtonSmall.js";
  import FilterPopup from "components/Popups/FilterPopup.js";
  import Header from "components/Headers/Header.js";
  import SalaryHistoryAdd from "./SalaryHistoryAdd.js";
  import LoadingOrError from "components/Notifications/LoadingOrError.js";
  
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
    const itemSingleFilters = [
      { labelName: "Employee Id", nameInput: "employeeId", type: "number" }
    ];
  
    //state constant
    const [pageIndex, setPageIndex] = useState(1);
    const [pageSize, setPageSize] = useState(5);
    const [dataFilter, setDataFilter] = useState({});
    const [sortBy, setSortBy] = useState("");
    const [bodyView, setBodyView] = useState("");
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
  
    const onConfirmFilter = (dataFilter) => {
      setPageIndex(1);
      setDataFilter(dataFilter);
    }
  
    const handleSelectSortByChange = (newItem) => {
      setSortBy(newItem);
    }
  
    const handleClickCreate = () => {
      setBodyView("create");
    }
  
    const handleCancelCreate = (event) => {
      event.preventDefault();
      setBodyView("table")
    }

    // request data
    const { data, loading, error } = useFilterSalaryHistory(dataFilter, sortBy, pageIndex, pageSize);
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
              {bodyView === "create"
                ? (<SalaryHistoryAdd onCancel={handleCancelCreate} />)
                : (
                  <Card className="shadow">
                    <CardHeader className="border-0">
                      <Container>
                        <Row>
                          <Col className="text-left">
                            <Button
                              className="btn-icon btn-3"
                              size="sm" color="primary"
                              type="button"
                              onClick={handleClickCreate}
                            >
                              <i className="fa-solid fa-plus mr-1"></i>
                              <span className="btn-inner--text m-0">Create</span>
                            </Button>
                          </Col>
                          <Col className="text-right">
                            <DropdownButtonSmall
                              selectedItemState={buttonSortByState}
                              viewValue={false}
                              arrayItems={arrayItemSortBy}
                              onSelectChange={handleSelectSortByChange}
                            />
                            <FilterPopup
                              itemSingleFilters={itemSingleFilters}
                              onConfirmFilter={onConfirmFilter}
                              dataFilterUseState={dataFilter}
                            />
                          </Col>
                        </Row>
                      </Container>
                    </CardHeader>
                    <Table className="align-items-center table-flush" responsive>
                      <thead className="thead-light">
                        <tr className="text-center">
                          <th scope="col">Id</th>
                          <th scope="col">Basic Salary</th>
                          <th scope="col">Bonus Salary</th>
                          <th scope="col">Penalty</th>
                          <th scope="col">Tax</th>
                          <th scope="col">Date<br/>(YYYY-MM-DD)</th>
                          <th scope="col">Employee</th>
                          <th scope="col" />
                        </tr>
                      </thead>
                      <tbody>
                        {data
                          ? data.results.map((item, index) => (
                            <tr key={index} className="text-center">
                              <th scope="row">{item.id}</th>
                              <td>{item.basicSalary}</td>
                              <td>{item.bonusSalary}</td>
                              <td>
                                {item.penalty}
                              </td>
                              <td>{item.tax}</td>
                              <td>{item.date.split("T")[0]}</td>
                              <td>{`${item.employeeId} - ${item.employeeName}`}</td>
                              <td className="text-right">
                                <UncontrolledDropdown>
                                  <DropdownToggle
                                    className="btn-icon-only text-light"
                                    href="#pablo"
                                    role="button"
                                    size="sm"
                                    color=""
                                    onClick={(e) => e.preventDefault()}
                                  >
                                    <i className="fas fa-ellipsis-v" />
                                  </DropdownToggle>
                                  <DropdownMenu className="dropdown-menu-arrow" right>
                                    <DropdownItem
                                      href={`salary-history/view?id=${item.id}`}
                                    >
                                      View Details
                                    </DropdownItem>
                                    <DropdownItem
                                      href={`salary-history/view?id=${item.id}&mode=edit`}
                                    >
                                      Edit
                                    </DropdownItem>
                                  </DropdownMenu>
                                </UncontrolledDropdown>
                              </td>
                            </tr>
                          ))
                          : ""}
                      </tbody>
                    </Table>
                    <CardFooter className="py-4">
                      <Container>
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
                )
              }
            </div>
          </Row>
        </Container>
      </>
    );
  };
  
  export default Tables;
  