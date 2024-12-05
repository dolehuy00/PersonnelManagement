import React from 'react';
import { Navigate } from 'react-router-dom';

function AuthRoute({ element }) {
    const accessToken = localStorage.getItem('accessToken');
    const userRole = localStorage.getItem('role');

    if (accessToken && userRole) {
        // Nếu đã đăng nhập và có role, chuyển hướng về trang của role
        return <Navigate to={`/${userRole.toLowerCase()}`} />;
    }

    // Nếu chưa đăng nhập, hiển thị trang đăng nhập (hoặc Component được truyền vào)
    return element;
}

export default AuthRoute;
