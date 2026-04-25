import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { House } from "../types/house";
import axios, { AxiosError, AxiosResponse, AxiosStatic } from "axios";
import config from "../config";
import { useNavigate } from "react-router-dom";

const useFetchHouses = () => {

    return useQuery<House[], AxiosError> ({
        queryKey: ["houses"],
        queryFn: () => axios(`${config.baseApiUrl}/houses`).then((res) => res.data)
    });
}

const useFetchHouse = (id: number) =>{
    return useQuery<House, AxiosError>({
        queryKey: ['houses', id],
        queryFn: () => axios(`${config.baseApiUrl}/house/${id}`).then((res) => res.data) 
    })
};

const useAddHouse = () =>{
    const nav = useNavigate();
    // Used to invalidate cache
    const queryClient = useQueryClient();
    return useMutation<AxiosResponse, AxiosError, House>({
        mutationFn: (h) => axios.post(`${config.baseApiUrl}/houses`, h),
        onSuccess: () => {
            queryClient.invalidateQueries({queryKey: ["houses"]});
            nav("/");
        }
    })
}

const useUpdateHouse = () =>{
    const nav = useNavigate();
    // Used to invalidate cache
    const queryClient = useQueryClient();
    return useMutation<AxiosResponse, AxiosError, House>({
        mutationFn: (h) => axios.put(`${config.baseApiUrl}/houses/${h.id}`, h),
        onSuccess: (_, house) => {
            queryClient.invalidateQueries({queryKey: ["houses"]});
            nav(`/house/${house.id}`);
        }
    })
}

const useDeleteHouse = () =>{
    const queryClient = useQueryClient();
    return useMutation<AxiosResponse, AxiosError, House>({
        mutationFn: (h) => axios.delete(`${config.baseApiUrl}/houses/${h.id}`),
        onSuccess: () => {
            queryClient.invalidateQueries({queryKey: ["houses"]});
        }
    })
}

export default useFetchHouses;
export { useFetchHouse, useAddHouse, useUpdateHouse, useDeleteHouse };
