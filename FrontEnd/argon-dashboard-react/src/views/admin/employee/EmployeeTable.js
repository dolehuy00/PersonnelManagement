
// reactstrap components
import {
  Badge,
  Card,
  CardHeader,
  CardFooter,
  DropdownMenu,
  DropdownItem,
  UncontrolledDropdown,
  DropdownToggle,
  Media,
  Table,
  Container,
  Row,
  Col,
  Button,
  Spinner
} from "reactstrap";

// core components
import React, { useState, useEffect } from "react";

// react toastify component
import { Slide, ToastContainer, toast } from 'react-toastify';

// hooks
import { useFilterEmployee, useChangeStatusEmployee } from "hooks/UseEmployeeApi.js";

//components
import CustomPagination from "components/Pagination/Pagination.js";
import DropdownButtonSmall from "components/Dropdowns/DropdownButtonSmall.js";
import FilterPopup from "components/Popups/FilterPopup.js";
import Header from "components/Headers/Header.js";
import EmployeeAdd from "./EmployeeAdd.js";
import LoadingOrError from "components/Notifications/LoadingOrError.js";
import ImageWithSkeleton from "components/Images/ImageWithSkeleton.js";

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
    { text: "Sort by fullname ascending", value: "fullname:asc" },
    { text: "Sort by fullname decreasing", value: "fullname:dec" },
    { text: "Sort by birthday ascending", value: "dateOfBirth:asc" },
    { text: "Sort by birthday decreasing", value: "dateOfBirth:dec" },
    { text: "Sort by start date ascending", value: "startDate:asc" },
    { text: "Sort by start date decreasing", value: "startDate:dec" }
  ];
  const itemSingleFilters = [
    { labelName: "Name Or Id", nameInput: "nameOrId", type: "text" },
    { labelName: "Address", nameInput: "address", type: "text" },
    { labelName: "Position", nameInput: "position", type: "text" },
    { labelName: "Department Id", nameInput: "departmentId", type: "number" }
  ];

  const itemRangeFilters = [
    { labelName: "Salary", nameInputFrom: "fromSalary", nameInputTo: "toSalary", type: "number" },
    { labelName: "Date Of Birth", nameInputFrom: "fromDoB", nameInputTo: "toDoB", type: "date" },
    { labelName: "Start Date", nameInputFrom: "fromStartDate", nameInputTo: "toStartDate", type: "date" }
  ];

  const itemSelectOptions = [
    {
        labelName: "Status",
        nameSelect: "status",
        Option: [
            { labelName: "Lock", value: "Lock" },
            { labelName: "Active", value: "Active" }
        ],
        type: "select_option"
    }
  ];

  //states
  const [pageIndex, setPageIndex] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [dataFilter, setDataFilter] = useState({});
  const [sortBy, setSortBy] = useState("");
  const [bodyView, setBodyView] = useState("");
  const buttonPerPageState = useState(arrayItemPerPage[0]);
  const buttonSortByState = useState(arrayItemSortBy[0])
  const [statusColMap, setStatusColMap] = useState({});

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

  const handleLockEmployee = (employeeId, statusValue) => {
    requestLock(employeeId, statusValue)
  }

  // request data
  const { data, loading, error } = useFilterEmployee(dataFilter, sortBy, pageIndex, pageSize);
  const totalPage = data.totalPage;
  const {
    data: dataLockResponse,
    loading: loadingLock,
    error: errorLock,
    request: requestLock
  } = useChangeStatusEmployee();


  // effect
  useEffect(() => {
    if (errorLock) {
      toast.error(`Change status fail, ${errorLock.response?.data?.messages[0]}`, {
        position: "bottom-right",
        autoClose: 10000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        progress: undefined,
        theme: "colored",
        transition: Slide,
      });
    }
  }, [errorLock]);

  useEffect(() => {
    if (dataLockResponse.status === 200) {
      changeStatusCol(
        dataLockResponse.messages[0],
        dataLockResponse.messages[1]
      );
      toast.success(
        `${dataLockResponse.messages[1] === "Active" ? "Unlock" : "Lock"} "${dataLockResponse.messages[0]}" successfully`,
        {
          position: "bottom-right",
          autoClose: 5000,
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
          theme: "colored",
          transition: Slide,
        }
      );
    }
  }, [dataLockResponse]);

  // fuction
  const changeStatusCol = (key, newStatus) => {
    setStatusColMap((prev) => ({ ...prev, [key]: newStatus }));
  };

  // render fuction
  const renderDataColStatus = (status) => {
    return (
      <>
        {status === "Active" ? (
          <i className="bg-success" />
        ) : (
          <i className="bg-danger" />
        )}
        {status}
      </>
    );
  }

  const renderContentItemLockDropdown = (emplId, status) => {
    return (
      <>
        {status === "Active"
          ? (
            <DropdownItem
              disabled={loadingLock}
              href="#pablo"
              onClick={(e) => {
                e.preventDefault();
                handleLockEmployee(emplId, status);
              }}
            >
              Lock
            </DropdownItem>
          )
          : (
            <DropdownItem
              disabled={loadingLock}
              href="#pablo"
              onClick={(e) => {
                e.preventDefault();
                handleLockEmployee(emplId, status)
              }}
            >
              Unlock
            </DropdownItem>
          )
        }
      </>
    )
  }

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
              ? (<EmployeeAdd onCancel={handleCancelCreate} />)
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
                            itemRangeFilters={itemRangeFilters}
                            onConfirmFilter={onConfirmFilter}
                            dataFilterUseState={dataFilter}
                            itemSelectOptions={itemSelectOptions}
                          />
                        </Col>
                      </Row>
                    </Container>
                  </CardHeader>
                  {loadingLock && (
                    <div className="overlay-spinner-table-loading">
                      <Spinner color="info"></Spinner>
                    </div>
                  )}
                  <Table className="align-items-center table-flush" responsive>
                    <thead className="thead-light">
                      <tr className="text-center">
                        <th scope="col">Id</th>
                        <th scope="col">Name</th>
                        <th scope="col">Basic Salary</th>
                        <th scope="col">Department</th>
                        <th scope="col">Status</th>
                        <th scope="col" />
                      </tr>
                    </thead>
                    <tbody>
                      {data
                        ? data.results.map((item, index) => (
                          <tr key={index} className="text-center">
                            <th scope="row">{item.id}</th>
                            <td>
                              <Media className="align-items-center">
                                <a
                                  className="avatar rounded-circle mr-3"
                                  href="#pablo"
                                  onClick={(e) => e.preventDefault()}
                                >
                                  <ImageWithSkeleton
                                    alt="avatar"
                                    placeholder={require("../../../assets/img/theme/img-gray.png")}
                                    src={item.image}
                                  />
                                </a>
                                <Media>
                                  <span className="mb-0 text-sm">
                                    {item.fullname}
                                  </span>
                                </Media>
                              </Media>
                            </td>
                            <td>{item.basicSalary}</td>
                            <td>
                              {item.departmentId === null
                                ? "No department"
                                : item.departmentName
                              }
                            </td>
                            <td>
                              <Badge
                                color=""
                                className="badge-dot mr-4">
                                {statusColMap[item.id]
                                  ? renderDataColStatus(statusColMap[item.id])
                                  : renderDataColStatus(item.status)
                                }
                              </Badge>
                            </td>
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
                                    href={`employee/view?id=${item.id}`}
                                  >
                                    View Details
                                  </DropdownItem>
                                  <DropdownItem
                                    href={`employee/view?id=${item.id}&mode=edit`}
                                  >
                                    Edit
                                  </DropdownItem>
                                  {statusColMap[item.id]
                                    ? renderContentItemLockDropdown(item.id, statusColMap[item.id])
                                    : renderContentItemLockDropdown(item.id, item.status)
                                  }
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
      <div>
        <ToastContainer />
      </div>
    </>
  );
};

export default Tables;
