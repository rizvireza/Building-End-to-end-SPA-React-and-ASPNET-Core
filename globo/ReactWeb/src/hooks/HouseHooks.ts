import { useQuery } from "@tanstack/react-query";
import { House } from "../types/house";
import axios, { AxiosError } from "axios";
import config from "../config";

const useFetchHouses = () => {

    return useQuery<House[], AxiosError> ({
        queryKey: ["houses"],
        queryFn: () => axios(`${config.baseApiUrl}/houses`).then((res) => res.data)
    });


}

export default useFetchHouses;
