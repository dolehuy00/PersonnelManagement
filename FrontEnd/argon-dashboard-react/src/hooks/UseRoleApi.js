import { useState } from 'react';
import { getAllRole } from 'services/management/RoleApi.js';
import { useNavigate } from "react-router-dom";

export const useGetAllRole = () => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    const requestGetAllRole = async () => {
        setLoading(true);
        try {
            const result = await getAllRole();
            setData(result);
            setLoading(false);
        } catch (error) {
            if (error.response.status === 401) {
                navigate('/auth');
            } else {
                setLoading(false);
                setError(error);
            }
        }
    };

    return { data, loading, error, requestGetAllRole };
};