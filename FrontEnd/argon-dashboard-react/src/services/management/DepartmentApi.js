import api from 'services/Api.js';

const API_BASE_URL = '/api/department';

export const getOneDepartment = async (departmentId) => {
    try {
        const accessToken = localStorage.getItem('accessToken');
        const response = await api.get(`${API_BASE_URL}/get/${departmentId}`, {
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

export const filterDepartment = async (dataFilter, sortBy, pageNumber, pageSize) => {
    try {
        const token = localStorage.getItem('accessToken');

        const response = await api.get(`${API_BASE_URL}/filter`, {
            headers: {
                'Authorization': `Bearer ${token}`
            },
            params: {
                Id: "id" in dataFilter ? dataFilter.id : "",
                Name: "name" in dataFilter ? dataFilter.name : "",            
                Status: "status" in dataFilter ? dataFilter.status : "",                
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

export const addDepartment = async (data) => {
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

export async function editDepartment(dataRequest) {
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

export async function lockDepartment(departmentId) {
    try {
        const token = localStorage.getItem('accessToken');

        const response = await api.put(`${API_BASE_URL}/changeStatus/${departmentId}`, null, {
            headers: {
                'Authorization': `Bearer ${token}`
            },
        });

        return response.data;
    } catch (error) {
        throw error;
    }
};

export async function unlockDepartment(departmentId) {
    try {
        const token = localStorage.getItem('accessToken');

        const response = await api.put(`${API_BASE_URL}/changeStatus/${departmentId}`, null, {
            headers: {
                'Authorization': `Bearer ${token}`
            },
        });

        return response.data;
    } catch (error) {
        throw error;
    }
};


export const searchDepartment = async (fullnameOrId) => {
    try {
        const accessToken = localStorage.getItem('accessToken');
        const isNumber = !isNaN(fullnameOrId); 
        const params = isNumber ? { id: fullnameOrId, name: fullnameOrId, status: 'Active' } : { name: fullnameOrId, status: 'Active' };
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

// {
//     "name": "Phòng Marketing",
//     "taskDetail": "Chịu trách nhiệm tiếp thị và quảng bá sản phẩm",
//     "status": "Active",
//     "leaderId": 3
//   }