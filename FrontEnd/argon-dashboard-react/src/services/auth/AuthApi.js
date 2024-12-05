import api from "services/Api.js";
import { saveImageToLocalStorage } from 'services/Image.js';

const API_BASE_URL = '/api/Account';

// Tạo biến toàn cục để quản lý trạng thái refresh token
let isRefreshing = false; // Cờ để kiểm tra trạng thái refresh
let refreshSubscribers = []; // Danh sách các yêu cầu đang chờ

// Hàm thêm yêu cầu vào hàng đợi
const subscribeTokenRefresh = (callback) => {
    refreshSubscribers.push(callback);
};

// Hàm xử lý tất cả các yêu cầu đang chờ khi token được làm mới
const onRefreshed = (newAccessToken) => {
    refreshSubscribers.forEach((callback) => callback(newAccessToken));
    refreshSubscribers = [];
};

async function refreshAccessToken() { 
    try {
        const response = await api.post(`${API_BASE_URL}/get-access-token`, null, { 
            withCredentials: true 
        });
        const newAccessToken = response.data.results[0].newAccessToken;
        // Lưu vào localStorage
        localStorage.setItem('accessToken', newAccessToken);

        return newAccessToken;
    } catch (error) {
        clearLocalStorage();
    }
}

api.interceptors.response.use(
    response => response,
    async error => {
        const originalRequest = error.config;
        if (error.response && error.response.status === 401 && !originalRequest._retry) {
            originalRequest._retry = true;
            
            // Nếu refresh token đang thực hiện, chờ token mới
            if (isRefreshing) {
                return new Promise((resolve) => {
                    subscribeTokenRefresh((newAccessToken) => {
                        originalRequest.headers['Authorization'] = `Bearer ${newAccessToken}`;
                        resolve(api(originalRequest));
                    });
                });
            }

            // Nếu không, bắt đầu refresh token
            isRefreshing = true;

            try {
                const newAccessToken = await refreshAccessToken(); // Hàm lấy token mới
                isRefreshing = false;

                // Cập nhật token mới trong tất cả các yêu cầu đang chờ
                onRefreshed(newAccessToken);

                // Cập nhật token vào header của yêu cầu ban đầu
                originalRequest.headers['Authorization'] = `Bearer ${newAccessToken}`;
                return api(originalRequest);
            } catch (refreshError) {
                isRefreshing = false;
                refreshSubscribers = []; // Xóa hàng đợi nếu refresh thất bại
                return Promise.reject(refreshError);
            }
        }
        return Promise.reject(error);
    }
);

function clearLocalStorage() {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('role');
    localStorage.removeItem('leaderOfDepartments');
    localStorage.removeItem('email');
    localStorage.removeItem('image');
}


export const login = async (email, password) => {
    try {
        const response = await api.post(API_BASE_URL + '/login', { email, password }, { 
            withCredentials: true 
        });
        const data = response.data;
        if (response.status === 200) {
            localStorage.setItem('email', data.results[0].email);
            localStorage.setItem('leaderOfDepartments',  JSON.stringify(data.results[0].leaderOfDepartments));
            localStorage.setItem('accessToken', data.results[0].accessToken);
            localStorage.setItem('role', data.results[0].role);
            saveImageToLocalStorage(data.results[0].employeeImage, "image")
            return data;
        } else {
            throw new Error(data.messages || 'Login failed');
        }
    } catch (error) {
        throw error;
    }
};

export const logout = async (navigate) => {
    try {
        const accessToken = localStorage.getItem('accessToken');
        await api.post(`${API_BASE_URL}/cancel-refressh-token`, null, {
            headers: {
                Authorization: `Bearer ${accessToken}`,
            },
            withCredentials: true
        });
    } catch (error) {
        console.error('Logout API error:', error);
    }
    clearLocalStorage();
    navigate('/auth');
};

export const forgotPassword = async (email) => {
    try {
        const response = await api.post(`${API_BASE_URL}/forgot-password-request`, { email });
        return response.data;
    } catch (error) {
        throw error;
    }
};

export const forgotPasswordVertifyCode = async (email, code) => {
    try {
        const response = await api.post(`${API_BASE_URL}/forgot-password-verify-code`, { email, code });
        return response.data;
    } catch (error) {
        throw error;
    }
};

export const forgotPasswordChangePassword = async (email, code, password, passwordConfirm) => {
    try {
        const response = await api.post(`${API_BASE_URL}/forgot-password-change`,
            { email, code, password, passwordConfirm });
        return response.data;
    } catch (error) {
        throw error;
    }
};

