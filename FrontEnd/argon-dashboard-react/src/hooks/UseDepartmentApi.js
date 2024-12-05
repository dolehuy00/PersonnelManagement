
import { useState, useEffect } from 'react';
import {
    filterDepartment,
    addDepartment,
    getOneDepartment,
    editDepartment,
    lockDepartment,
    unlockDepartment,
    searchDepartment
} from 'services/management/DepartmentApi.js';
import { useNavigate } from "react-router-dom";

export const useFilterDepartment = (dataFilter, sortBy, page, pageSize) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const getData = async () => {
            setLoading(true);
            try {
                const result = await filterDepartment(dataFilter, sortBy, page, pageSize);
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
        getData();
    }, [dataFilter, sortBy, page, pageSize, navigate]);

    return { data, loading, error };
};

export const useAddDepartment = (dataBody) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const request = async () => {
            setLoading(true);
            try {
                if (Object.keys(dataBody).length > 0) {
                    const result = await addDepartment(dataBody);
                    setData(result);
                }
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
        request();
    }, [dataBody, navigate]);

    return { data, loading, error };
};

export const useGetOneDepartment = (departmentId) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const request = async () => {
            setLoading(true);
            try {
                const result = await getOneDepartment(departmentId);
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
        request();
    }, [departmentId, navigate]);

    return { data, loading, error };
};

export const useEditDepartment = (dataBody) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const request = async () => {
            setLoading(true);
            try {
                if (Object.keys(dataBody).length > 0) {
                    const result = await editDepartment(dataBody);
                    setData(result);
                }
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
        request();
    }, [dataBody, navigate]);

    return { data, loading, error };
};

export const useChangeStatusDepartment = () => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    const request = async (departmentId, curentStatus) => {
        setLoading(true);
        try {
            if (curentStatus === "Active") {
                const result = await lockDepartment(departmentId);
                setData(result);
            } else {
                const result = await unlockDepartment(departmentId);
                setData(result);
            }
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

    return { data, loading, error, request };
};

export const useSearchDepartment = () => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    const requestSearch = async (fullnameOrId) => {
        setLoading(true);
        try {
            if (fullnameOrId.length > 0) {
                const result = await searchDepartment(fullnameOrId);
                setData(result.results);
            }
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

    return { data, loading, error, requestSearch };
};