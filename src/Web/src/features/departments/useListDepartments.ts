import { useQuery } from "@tanstack/react-query";
import departmentService from "./department.service";

export default function useListDepartments() {
    return useQuery({
        queryKey: ["departments"],
        queryFn: () => departmentService.listDepartments(),
    })
}