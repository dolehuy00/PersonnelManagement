import api from 'services/Api.js';

const API_BASE_URL = '/api/Assignment';

export const getOneAssignment = async (assignmentId) => {
    try {
        const accessToken = localStorage.getItem('accessToken');
        const response = await api.get(`${API_BASE_URL}/get/${assignmentId}`, {
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

export const getOneAssignmentByUser = async (assignmentId) => {
    try {
        const accessToken = localStorage.getItem('accessToken');
        const response = await api.get(`${API_BASE_URL}/get-by-user/${assignmentId}`, {
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

export const getOneAssignmentByLeader = async (departmentId, assignmentId) => {
    try {
        const accessToken = localStorage.getItem('accessToken');
        const response = await api.get(`${API_BASE_URL}/get-by-leader?id=${assignmentId}&departmentId=${departmentId}`, {
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

export const filterAssignment = async (dataFilter, sortBy, pageNumber, pageSize) => {
    try {
        const token = localStorage.getItem('accessToken');

        const response = await api.get(`${API_BASE_URL}/filter`, {
            headers: {
                'Authorization': `Bearer ${token}`
            },
            params: {
                status: "status" in dataFilter ? dataFilter.status : "",
                responsiblePesonId: "responsiblePesonId" in dataFilter ? dataFilter.responsiblePesonId : "",
                projectId: "projectId" in dataFilter ? dataFilter.projectId : "",
                DepartmentId: "departmentId" in dataFilter ? dataFilter.departmentId : "",
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

export const filterAssignmentByUser = async (dataFilter, sortBy, pageNumber, pageSize) => {
    try {
        const token = localStorage.getItem('accessToken');

        const response = await api.get(`${API_BASE_URL}/filter-by-user`, {
            headers: {
                'Authorization': `Bearer ${token}`
            },
            params: {
                status: "status" in dataFilter ? dataFilter.status : "",
                projectId: "projectId" in dataFilter ? dataFilter.projectId : "",
                DepartmentId: "departmentId" in dataFilter ? dataFilter.departmentId : "",
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

export const filterAssignmentByLeader = async (departmentId, deptAssignmentId, dataFilter, sortBy, pageNumber, pageSize) => {
    try {
        const token = localStorage.getItem('accessToken');

        const response = await api.get(`${API_BASE_URL}/filter-by-leader`, {
            headers: {
                'Authorization': `Bearer ${token}`
            },
            params: {
                status: "status" in dataFilter ? dataFilter.status : "",
                responsiblePesonId: "responsiblePesonId" in dataFilter ? dataFilter.responsiblePesonId : "",
                projectId: "projectId" in dataFilter ? dataFilter.projectId : "",
                DepartmentId: departmentId,
                DeptAssignmentId: deptAssignmentId,
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

export const addAssignment = async (data) => {
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

export const addAssignmentByLeader = async (departmentId, data) => {
    try {
        const token = localStorage.getItem('accessToken');

        const response = await api.post(`${API_BASE_URL}/add-by-leader/${departmentId}`, data, {
            headers: {
                'Authorization': `Bearer ${token}`
            },
        });

        return response.data;

    } catch (error) {
        throw error;
    }
};

export async function editAssignment(dataRequest) {
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

export async function editAssignmentByLeader(departmentId, dataRequest) {
    try {
        const token = localStorage.getItem('accessToken');

        const response = await api.put(`${API_BASE_URL}/edit-by-leader/${departmentId}`, dataRequest, {
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

export async function changeStatusByUser(assignmentId, status) {
    try {
        const token = localStorage.getItem('accessToken');

        const response = await api.put(`${API_BASE_URL}/change-status-by-user?id=${assignmentId}&status=${status}`, null, {
            headers: {
                'Authorization': `Bearer ${token}`
            },
        });

        return response.data;
    } catch (error) {
        throw error;
    }
};

