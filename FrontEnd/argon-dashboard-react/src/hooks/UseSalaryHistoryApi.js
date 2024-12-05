
import { useState, useEffect } from 'react';
import {
    filterSalaryHistory,
    addSalaryHistory,
    getOneSalaryHistory,
    editSalaryHistory,
    getPageSalaryHistoryByUser,
    filterSalaryHistoryByUser,
    getOneSalaryHistoryByUser
} from 'services/management/SalaryHistoryApi.js';
import { useNavigate } from "react-router-dom";

export const useFilterSalaryHistory = (dataFilter, sortByDate, page, pageSize) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const getData = async () => {
            setLoading(true);
            try {
                const result = await filterSalaryHistory(dataFilter, sortByDate, page, pageSize);
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
    }, [dataFilter, sortByDate, page, pageSize, navigate]);

    return { data, loading, error };
};

export const useFilterSalaryHistoryByUser = (sortByDate, page, pageSize) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const getData = async () => {
            setLoading(true);
            try {
                const result = await filterSalaryHistoryByUser(sortByDate, page, pageSize);
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
    }, [sortByDate, page, pageSize, navigate]);

    return { data, loading, error };
};

export const useAddSalaryHistory = (dataBody) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const request = async () => {
            setLoading(true);
            try {
                if (Object.keys(dataBody).length > 0) {
                    const result = await addSalaryHistory(dataBody);
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

export const useGetOneSalaryHistory = (salaryHistoryId) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const request = async () => {
            setLoading(true);
            try {
                const result = await getOneSalaryHistory(salaryHistoryId);
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
    }, [salaryHistoryId, navigate]);

    return { data, loading, error };
};

export const useGetOneSalaryHistoryByUser = (salaryHistoryId) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const request = async () => {
            setLoading(true);
            try {
                const result = await getOneSalaryHistoryByUser(salaryHistoryId);
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
    }, [salaryHistoryId, navigate]);

    return { data, loading, error };
};


export const useGetPageSalaryHistoryByUser = (page, pageSize) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const request = async () => {
            setLoading(true);
            try {
                const data = await getPageSalaryHistoryByUser(page, pageSize);
                setData(data);
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
    }, [page, pageSize, navigate]);

    return { data, loading, error };
};

export const useEditSalaryHistory = (dataBody) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const request = async () => {
            setLoading(true);
            try {
                if (Object.keys(dataBody).length > 0) {
                    const result = await editSalaryHistory(dataBody);
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