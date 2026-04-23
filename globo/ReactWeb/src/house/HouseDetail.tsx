import { useParams } from "react-router-dom";
import { useFetchHouse } from "../hooks/HouseHooks";
import ApiStatus from "../apiStatus";

const HouseDetail = () =>{
    const {id} = useParams();
    if (!id) throw new Error("House id is not found.");
    const houseId = parseInt(id);
    const {data, status, isSuccess} = useFetchHouse(houseId);
    if(!isSuccess) return <ApiStatus status={status} />;
    if(!data) return <div>House not found</div>
    
    return (<></>);
}

export default HouseDetail;