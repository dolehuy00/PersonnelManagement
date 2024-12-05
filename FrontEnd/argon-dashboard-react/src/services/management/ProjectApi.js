import api from "services/Api.js";
//import MockAdapter from 'axios-mock-adapter';

const API_BASE_URL = '/api/project';

export const getOneProject = async (projectId) => {
    try {
        const accessToken = localStorage.getItem('accessToken');
        const response = await api.get(`${API_BASE_URL}/get/${projectId}`, {
            headers: {
                'Authorization': `Bearer ${accessToken}`
            },
        });
        
        var data = response.data;
        data.results[0].startDate = data.results[0].startDate.split("T")[0];
        data.results[0].duration = data.results[0].duration.split("T")[0];
        return data;
    } catch (error) {
        throw error;
    }
}

export const filterProject = async (dataFilter, sortBy, pageNumber, pageSize) => {
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
                StartDate: "StartDate" in dataFilter ? dataFilter.StartDate : "",       
                EndDate: "EndDate" in dataFilter ? dataFilter.EndDate : "",                
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

export const addProject = async (data) => {
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

export async function editProject(dataRequest) {
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

export async function lockProject(projectId) {
    try {
        const token = localStorage.getItem('accessToken');

        const response = await api.put(`${API_BASE_URL}/changeStatus/${projectId}`, null, {
            headers: {
                'Authorization': `Bearer ${token}`
            },
        });

        return response.data;
    } catch (error) {
        throw error;
    }
};

export async function unlockProject(projectId) {
    try {
        const token = localStorage.getItem('accessToken');

        const response = await api.put(`${API_BASE_URL}/changeStatus/${projectId}`, null, {
            headers: {
                'Authorization': `Bearer ${token}`
            },
        });

        return response.data;
    } catch (error) {
        throw error;
    }
};


export const searchProject = async (fullnameOrId) => {
    try {
        const accessToken = localStorage.getItem('accessToken');
        const response = await api.get(`${API_BASE_URL}/search`, {
            params: { fullnameOrId },
            headers: {
                'Authorization': `Bearer ${accessToken}`
            },
        });

        var data = response.data;
        for (let index = 0; index < data?.results?.length; index++) {
            data.results[0].startDate = data.results[0].startDate.split("T")[0];
            data.results[0].duration = data.results[0].duration.split("T")[0]; 
        }
        
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