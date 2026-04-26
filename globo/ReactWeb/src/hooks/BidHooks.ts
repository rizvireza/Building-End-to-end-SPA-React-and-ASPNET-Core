import axios, { Axios, AxiosError } from "axios"
import { Bid } from "../types/bid";
import Problem from "../types/problem";
import config from "../config";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";

const useFetchBids = (houseId: number) => {
    return useQuery<Bid[], AxiosError<Problem>>({
        queryKey: ["bids", houseId],
        queryFn: async () => {
            const response = await axios.get(`${config.baseApiUrl}/houses/${houseId}/bids`);
            return response.data;
        }
    })
}

const useAddBids = () => {
    const queryClient = useQueryClient();
    return useMutation<Bid, AxiosError<Problem>, Bid>({
        mutationFn: (b) => axios.post(`${config.baseApiUrl}/houses/${b.houseId}/bids`, b),
        onSuccess: (resp, bid) => {
            queryClient.invalidateQueries({ queryKey: ["bids", bid.houseId] });
        }
    });
}

export {useFetchBids, useAddBids};