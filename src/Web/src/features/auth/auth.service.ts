import HttpService from "@libs/services/http.service"

import {
  AuthUser,
  LoginRequest,
  LoginResponse
} from "./auth.type"

class AuthService extends HttpService {
  constructor() {
    super()
  }

  login(data: LoginRequest): Promise<LoginResponse> {
    console.log(data)
    return this.post("/Users/login", data)
  }

  getMe(): Promise<AuthUser> {
    return this.get("/Users/me")
  }
}

export default new AuthService()
