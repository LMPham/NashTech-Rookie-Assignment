import HttpService from "@libs/services/http.service"
import { Department } from "./department.type"

class DepartmentService extends HttpService {
  constructor() {
    super()
  }

  listDepartments(): Promise<Department[]> {
    return this.get(`/Departments`)
  }

//   createDepartment(data: CreateDepartmentRequest): Promise<string> {
//     const formattedData = {
//       ...data,
//       installDate: format(data.installDate, "yyyy-MM-dd"),
//     }
//     return this.post("/assets", formattedData)
//   }

//   deleteDepartment(id: string): Promise<void> {
//     return this.delete(`/assets/${id}`)
//   }

//   updateDepartment(data: UpdateDepartmentRequest): Promise<void> {
//     const formattedData = {
//       ...data,
//       installDate: format(data.installDate, "yyyy-MM-dd"),
//     }
//     return this.put("/assets", formattedData)
//   }
}

export default new DepartmentService()
