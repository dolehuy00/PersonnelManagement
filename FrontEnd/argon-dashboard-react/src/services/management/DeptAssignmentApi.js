import api from "services/Api.js";
//import MockAdapter from 'axios-mock-adapter';

const API_BASE_URL = '/api/deptassignment';

export const getOneDeptAssignment = async (deptassignmentId) => {
    try {
        const accessToken = localStorage.getItem('accessToken');
        const response = await api.get(`${API_BASE_URL}/get/${deptassignmentId}`, {
            headers: {
                'Authorization': `Bearer ${accessToken}`
            },
        });

        var data = response.data;
        return data;
    } catch (error) {
        throw error;
    }
}

export const filterDeptAssignment = async (dataFilter, sortBy, pageNumber, pageSize) => {
    try {
        const token = localStorage.getItem('accessToken');

        const response = await api.get(`${API_BASE_URL}/filter`, {
            headers: {
                'Authorization': `Bearer ${token}`
            },
            params: {
                id: "id" in dataFilter ? dataFilter.id : "",
                projectId: "projectId" in dataFilter ? dataFilter.projectId : "",            
                departmentId: "departmentId" in dataFilter ? dataFilter.departmentId : "",           
                SortBy: sortBy,
                Page: pageNumber,
                PageSize: pageSize
            }
        });
        return response.data;
    } catch (error) {
        throw error;
    }
};


export const filterDeptAssignmentByLeader = async (departmentId, dataFilter, sortBy, pageNumber, pageSize) => {
    try {
        const token = localStorage.getItem('accessToken');

        const response = await api.get(`${API_BASE_URL}/filter-by-leader`, {
            headers: {
                'Authorization': `Bearer ${token}`
            },
            params: {
                id: "id" in dataFilter ? dataFilter.id : "",
                projectId: "projectId" in dataFilter ? dataFilter.projectId : "",            
                departmentId: departmentId,
                SortBy: sortBy,
                Page: pageNumber,
                PageSize: pageSize
            }
        });
        return response.data;
    } catch (error) {
        throw error;
    }
};

export const getDeptsAssignmentsByProject = async (projectId) => {
    try {
        const token = localStorage.getItem('accessToken');
        
        const response = await api.get(`${API_BASE_URL}/filter`, {
            headers: {
                'Authorization': `Bearer ${token}`
            },
            params: {
                projectId: projectId ? projectId : 0
            }
        });
        console.log(response.data);
        return response.data;
    } catch (error) {
        console.log(error);
        throw error;
    }
};



export const addManyDeptAssignment = async (deptAssignments) => {
    try {
        const token = localStorage.getItem('accessToken');

        const response = await api.post(`${API_BASE_URL}/addMany`, deptAssignments, {
            headers: {
                'Authorization': `Bearer ${token}`
            },
        });

        return response.data;

    } catch (error) {
        throw error;
    }
};

export const editManyDeptAssignment = async (projectId, deptAssignments) => {
    try {
        const token = localStorage.getItem('accessToken');

        const response = await api.put(`${API_BASE_URL}/editMany/${projectId}`, deptAssignments, {
            headers: {
                'Authorization': `Bearer ${token}`
            },
        });

        return response.data;

    } catch (error) {
        throw error;
    }
};


export const addDeptAssignment = async (data) => {
    try {
        const token = localStorage.getItem('accessToken');

        const response = await api.post(`${API_BASE_URL}/add`, data, {
            headers: {
                'Authorization': `Bearer ${token}`
            },
        });

        return response.data;

    } catch (error) {
        throw error;
    }
};

export async function editDeptAssignment(dataRequest) {
    try {
        const token = localStorage.getItem('accessToken');

        const response = await api.put(`${API_BASE_URL}/edit`, dataRequest, {
            headers: {
                'Authorization': `Bearer ${token}`
            },
        });

        var dataResponse = response.data;

        return dataResponse;

    } catch (error) {
        throw error;
    }
};

export async function lockDeptAssignment(deptassignmentId) {
    try {
        const token = localStorage.getItem('accessToken');

        const response = await api.put(`${API_BASE_URL}/changeStatus/${deptassignmentId}`, null, {
            headers: {
                'Authorization': `Bearer ${token}`
            },
        });

        return response.data;
    } catch (error) {
        throw error;
    }
};

export async function unlockDeptAssignment(deptassignmentId) {
    try {
        const token = localStorage.getItem('accessToken');

        const response = await api.put(`${API_BASE_URL}/changeStatus/${deptassignmentId}`, null, {
            headers: {
                'Authorization': `Bearer ${token}`
            },
        });

        return response.data;
    } catch (error) {
        throw error;
    }
};


export const searchDeptAssignment = async (fullnameOrId) => {
    try {
        const accessToken = localStorage.getItem('accessToken');
        const isNumber = !isNaN(fullnameOrId); 
        const params = isNumber ? { id: fullnameOrId, name: fullnameOrId } : { name: fullnameOrId };
        const response = await api.get(`${API_BASE_URL}/filter`, {
            params: params,
            headers: {
                'Authorization': `Bearer ${accessToken}`
            },
        });
        var data = response.data;
        return data;
    } catch (error) {
        throw error;
    }
}

export const searchId = async (id) => {
    try {
        const accessToken = localStorage.getItem('accessToken');
        const response = await api.get(`${API_BASE_URL}/search`, {
            params: { id },
            headers: {
                'Authorization': `Bearer ${accessToken}`
            },
        });

        var data = response.data;
        return data;
    } catch (error) {
        throw error;
    }
}

// {
//     "name": "Phòng Marketing",
//     "taskDetail": "Chịu trách nhiệm tiếp thị và quảng bá sản phẩm",
//     "status": "Active",
//     "leaderId": 3
//   }