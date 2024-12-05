import { Navigate } from 'react-router-dom';

function ProtectedRoute({ element, allowedRoles }) {
  const accessToken = localStorage.getItem('accessToken');
  const userRole = localStorage.getItem('role');

  if (!accessToken) {
    // Chuyển hướng đến trang đăng nhập nếu chưa đăng nhập
    return <Navigate to="/auth/login" replace />;
  }

  if (allowedRoles && !allowedRoles.includes(userRole)) {
    // Chuyển hướng về dashboard nếu role không đúng
    return <Navigate to={`/${userRole.toLowerCase()}`} replace />;
  }

  // Hiển thị trang nếu người dùng đã đăng nhập và có đúng quyền
  return element;
}

export default ProtectedRoute;