
import { useState, useEffect } from 'react';
import {
    filterDeptAssignment,
    addDeptAssignment,
    getDeptsAssignmentsByProject,
    getOneDeptAssignment,
    editDeptAssignment,
    lockDeptAssignment,
    unlockDeptAssignment,
    searchDeptAssignment,
    addManyDeptAssignment,
    editManyDeptAssignment,
    searchId,
    filterDeptAssignmentByLeader
} from 'services/management/DeptAssignmentApi.js';
import { useNavigate } from "react-router-dom";

export const useFilterDeptAssignment = (dataFilter, sortBy, page, pageSize) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const getData = async () => {
            setLoading(true);
            try {
                const result = await filterDeptAssignment(dataFilter, sortBy, page, pageSize);
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


export const useFilterDeptAssignmentByLeader = (departmentId, dataFilter, sortBy, page, pageSize) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const getData = async () => {
            setLoading(true);
            try {
                const result = await filterDeptAssignmentByLeader(departmentId, dataFilter, sortBy, page, pageSize);
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
    }, [departmentId, dataFilter, sortBy, page, pageSize, navigate]);

    return { data, loading, error };
};


export const useGetDeptsAssignmentsByProject = (projectId) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const getData = async () => {
            setLoading(true);
            try {
                
                const result = await getDeptsAssignmentsByProject(projectId);
                setData(result);
                setLoading(false);
            } catch (error) {
                if (error.status === 401) {
                    navigate('/auth');
                } else {
                    setLoading(false);
                    setError(error);
                }
            }
        };
        getData();
    }, [projectId, navigate]);

    return { data, loading, error };
};

export const useAddDeptAssignment = (dataBody) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const request = async () => {
            setLoading(true);
            try {
                if (Object.keys(dataBody).length > 0) {
                    const result = await addDeptAssignment(dataBody);
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


export const useAddManyDeptAssignment = (deptAssignments) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    useEffect(() => {
        
        const request = async () => {
            setLoading(true);
            try {
                if (Object.keys(deptAssignments).length > 0) {
                    const result = await addManyDeptAssignment(deptAssignments);
                    setData(result);
                }
                setLoading(false);
            } catch (error) {
                if (error.status === 401) {
                    navigate('/auth');
                } else {
                    setLoading(false);
                    setError(error);
                }
            }
        };
        request();
    }, [deptAssignments, navigate]);

    return { data, loading, error };
};


export const useEditManyDeptAssignment = (projectId, deptAssignments) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    useEffect(() => {
        
        const request = async () => {
            setLoading(true);
            try {
                if (Object.keys(deptAssignments).length > 0) {
                    const result = await editManyDeptAssignment(projectId, deptAssignments);
                    setData(result);
                }
                setLoading(false);
            } catch (error) {
                if (error.status === 401) {
                    navigate('/auth');
                } else {
                    setLoading(false);
                    setError(error);
                }
            }
        };
        request();
    }, [deptAssignments, navigate]);

    return { data, loading, error };
};

export const useGetOneDeptAssignment = (deptassignmentId) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const request = async () => {
            setLoading(true);
            try {
                const result = await getOneDeptAssignment(deptassignmentId);
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
    }, [deptassignmentId, navigate]);

    return { data, loading, error };
};

export const useEditDeptAssignment = (dataBody) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const request = async () => {
            setLoading(true);
            try {
                if (Object.keys(dataBody).length > 0) {
                    const result = await editDeptAssignment(dataBody);
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

export const useChangeStatusDeptAssignment = () => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    const request = async (deptassignmentId, curentStatus) => {
        setLoading(true);
        try {
            if (curentStatus === "Active") {
                const result = await lockDeptAssignment(deptassignmentId);
                setData(result);
            } else {
                const result = await unlockDeptAssignment(deptassignmentId);
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

export const useSearchDeptAssignment = () => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    const requestSearch = async (fullnameOrId) => {
        setLoading(true);
        try {
            if (fullnameOrId.length > 0) {
                const result = await searchDeptAssignment(fullnameOrId);
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

export const useSearchIdDeptAssignment = () => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    const requestSearch = async (id) => {
        setLoading(true);
        try {
            if (id.length > 0) {
                const result = await searchId(id);
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