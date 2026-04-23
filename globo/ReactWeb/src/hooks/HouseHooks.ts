import { useQuery } from "@tanstack/react-query";
import { House } from "../types/house";
import axios, { AxiosError, AxiosStatic } from "axios";
import config from "../config";

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

export default useFetchHouses;
export { useFetchHouse };
