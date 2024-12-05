
import { useState, useEffect } from 'react';
import {
    filterAssignment,
    addAssignment,
    getOneAssignment,
    editAssignment,
    filterAssignmentByLeader,
    addAssignmentByLeader,
    editAssignmentByLeader,
    getOneAssignmentByLeader,
    filterAssignmentByUser,
    getOneAssignmentByUser,
    changeStatusByUser
} from 'services/management/AssignmentApi.js';
import { useNavigate } from "react-router-dom";

export const useFilterAssignment = (dataFilter, sortBy, page, pageSize) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const getData = async () => {
            setLoading(true);
            try {
                const result = await filterAssignment(dataFilter, sortBy, page, pageSize);
                setData(result);
                setLoading(false);
            } catch (error) {
                if (error.response?.status === 401) {
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

export const useFilterAssignmentByUser = (dataFilter, sortBy, page, pageSize) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const getData = async () => {
            setLoading(true);
            try {
                const result = await filterAssignmentByUser(dataFilter, sortBy, page, pageSize);
                setData(result);
                setLoading(false);
            } catch (error) {
                if (error.response?.status === 401) {
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

export const useFilterAssignmentByLeader = (departmentId, deptAssignmentId, dataFilter, sortBy, page, pageSize) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const getData = async () => {
            setLoading(true);
            try {
                const result = await filterAssignmentByLeader(departmentId, deptAssignmentId, dataFilter, sortBy, page, pageSize);
                setData(result);
                setLoading(false);
            } catch (error) {
                if (error.response?.status === 401) {
                    navigate('/auth');
                } else {
                    setLoading(false);
                    setError(error);
                }
            }
        };
        getData();
    }, [departmentId, deptAssignmentId, dataFilter, sortBy, page, pageSize, navigate]);

    return { data, loading, error };
};

export const useAddAssignment = (dataBody) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const request = async () => {
            setLoading(true);
            try {
                if (Object.keys(dataBody).length > 0) {
                    const result = await addAssignment(dataBody);
                    setData(result);
                }
                setLoading(false);
            } catch (error) {
                if (error.response?.status === 401) {
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


export const useAddAssignmentByLeader = (departmentId, dataBody) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const request = async () => {
            setLoading(true);
            try {
                if (Object.keys(dataBody).length > 0) {
                    const result = await addAssignmentByLeader(departmentId, dataBody);
                    setData(result);
                }
                setLoading(false);
            } catch (error) {
                if (error.response?.status === 401) {
                    navigate('/auth');
                } else {
                    setLoading(false);
                    setError(error);
                }
            }
        };
        request();
    }, [departmentId, dataBody, navigate]);

    return { data, loading, error };
};

export const useGetOneAssignment = (assignmentId) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const request = async () => {
            setLoading(true);
            try {
                const result = await getOneAssignment(assignmentId);
                setData(result);
                setLoading(false);
            } catch (error) {
                if (error.response?.status === 401) {
                    navigate('/auth');
                } else {
                    setLoading(false);
                    setError(error);
                }
            }
        };
        request();
    }, [assignmentId, navigate]);

    return { data, loading, error };
};

export const useGetOneAssignmentByUser = (assignmentId) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const request = async () => {
            setLoading(true);
            try {
                const result = await getOneAssignmentByUser(assignmentId);
                setData(result);
                setLoading(false);
            } catch (error) {
                if (error.response?.status === 401) {
                    navigate('/auth');
                } else {
                    setLoading(false);
                    setError(error);
                }
            }
        };
        request();
    }, [assignmentId, navigate]);

    return { data, loading, error };
};
export const useGetOneAssignmentByLeader = (departmentId, assignmentId) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const request = async () => {
            setLoading(true);
            try {
                const result = await getOneAssignmentByLeader(departmentId, assignmentId);
                setData(result);
                setLoading(false);
            } catch (error) {
                if (error.response?.status === 401) {
                    navigate('/auth');
                } else {
                    setLoading(false);
                    setError(error);
                }
            }
        };
        request();
    }, [departmentId, assignmentId, navigate]);

    return { data, loading, error };
};

export const useEditAssignment = (dataBody) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const request = async () => {
            setLoading(true);
            try {
                if (Object.keys(dataBody).length > 0) {
                    const result = await editAssignment(dataBody);
                    setData(result);
                }
                setLoading(false);
            } catch (error) {
                if (error.response?.status === 401) {
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

export const useEditAssignmentByLeader = (departmentId, dataBody) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const request = async () => {
            setLoading(true);
            try {
                if (Object.keys(dataBody).length > 0) {
                    const result = await editAssignmentByLeader(departmentId, dataBody);
                    setData(result);
                }
                setLoading(false);
            } catch (error) {
                if (error.response?.status === 401) {
                    navigate('/auth');
                } else {
                    setLoading(false);
                    setError(error);
                }
            }
        };
        request();
    }, [departmentId, dataBody, navigate]);

    return { data, loading, error };
};

export const useChangeStatusByUser = () => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    const request = async (assignmentId, status) => {
        setLoading(true);
        try {
            const result = await changeStatusByUser(assignmentId, status);
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

    return { data, loading, error, request };
};