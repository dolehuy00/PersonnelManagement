/*!

=========================================================
* Argon Dashboard React - v1.2.4
=========================================================

* Product Page: https://www.creative-tim.com/product/argon-dashboard-react
* Copyright 2024 Creative Tim (https://www.creative-tim.com)
* Licensed under MIT (https://github.com/creativetimofficial/argon-dashboard-react/blob/master/LICENSE.md)

* Coded by Creative Tim

=========================================================

* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

*/
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
import { useFilterProject, useChangeStatusProject } from "hooks/UseProjectApi.js";
//components
import CustomPagination from "components/Pagination/Pagination.js";
import DropdownButtonSmall from "components/Dropdowns/DropdownButtonSmall.js";
import FilterPopup from "components/Popups/FilterPopup.js";
import Header from "components/Headers/Header.js";
import ProjectAdd from "./ProjectAdd.js";
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
    { text: "No sort", value: "id:asc" },
    { text: "Sort by id ascending", value: "id:asc" },
    { text: "Sort by id decreasing", value: "id:dec" },
    { text: "Sort by name ascending", value: "name:asc" },
    { text: "Sort by name decreasing", value: "name:dec" },
    { text: "Sort by start date ascending", value: "startDate:asc" },
    { text: "Sort by start date decreasing", value: "startDate:dec" },
    { text: "Sort by duration ascending", value: "duration:asc" },
    { text: "Sort by duration decreasing", value: "duration:dec" },
  ];
  const itemSingleFilters = [
    { labelName: "Id", nameInput: "id", type: "text" },
    { labelName: "Name", nameInput: "name", type: "text" }
  ];
  const itemRangeFilters = [
    { labelName: "Start Date", nameInputFrom: "StartDate", nameInputTo: "EndDate", type: "date" }
  ];
  const itemSelectOptions = [
    {
        labelName: "Status",
        nameSelect: "status",
        Option: [
            { labelName: "Not Started", value: "Not Started" },
            { labelName: "In Progress", value: "In Progress" },
            { labelName: "On Hold", value: "On Hold" },
            { labelName: "Completed", value: "Completed" },
            { labelName: "Cancelled", value: "Cancelled" },
        ],
        type: "select_option"
    }
  ];
  //states
  const [pageIndex, setPageIndex] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [dataFilter, setDataFilter] = useState({});
  const [sortBy, setSortBy] = useState("id:asc");
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

  const handleLockProject = (projectId, statusValue) => {
    requestLock(projectId, statusValue)
  }

  // request data
  const { data, loading, error } = useFilterProject(dataFilter, sortBy, pageIndex, pageSize);
  const totalPage = data.totalPage;
  const {
    data: dataLockResponse,
    loading: loadingLock,
    error: errorLock,
    request: requestLock
  } = useChangeStatusProject();


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
    let statusClass = ""; // Lớp CSS mặc định
    switch (status) {
      case "Not Started":
        statusClass = "bg-secondary"; // Trắng
        break;
      case "In Progress":
        statusClass = "bg-primary"; // Xanh Dương
        break;
      case "On Hold":
        statusClass = "bg-secondary"; // Xám
        break;
      case "Completed":
        statusClass = "bg-success"; // Vàng
        break;
      case "Cancelled":
        statusClass = "bg-danger"; // Đỏ
        break;
      default:
        statusClass = "bg-light"; // Trạng thái không xác định
    }
  
    return (
      <>
        <i
          className={statusClass}      
          style={{
            display: "inline-block",
            width: "10px",
            height: "10px",
            borderRadius: "50%",
            marginRight: "5px",
          }}    
        />
        {status}
      </>
    );
  };
  

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
                handleLockProject(emplId, status);
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
                handleLockProject(emplId, status)
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
              ? (<ProjectAdd onCancel={handleCancelCreate} />)
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
                            itemSelectOptions={itemSelectOptions}
                            onConfirmFilter={onConfirmFilter}
                            dataFilterUseState={dataFilter}
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
                        <th scope="col">Start Date</th>
                        <th scope="col">Duration</th>                        
                        <th scope="col">Detail</th>                        
                        <th scope="col">Status</th>
                        <th scope="col" />
                      </tr>
                    </thead>
                    <tbody>
                      {data
                        ? data.results.map((item, index) => (
                          <tr key={index} className="text-center">
                            <th scope="row">{item.id}</th>
                            <td>{item.name}</td>                            
                            <td>{item.startDate.split("T")[0]}</td>
                            <td>{item.duration.split("T")[0]}</td>
                            <td>{item.detail}</td>
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
                                    href={`project/view?id=${item.id}`}
                                  >
                                    View
                                  </DropdownItem>
                                  <DropdownItem
                                    href={`project/view?id=${item.id}&mode=edit`}
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
      <div>
        <ToastContainer />
      </div>
    </>
  );
};

export default Tables;
