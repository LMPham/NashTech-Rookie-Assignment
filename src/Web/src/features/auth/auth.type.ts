export type LoginRequest = {
  email: string
  password: string
}

export type LoginResponse = {
  tokenType: string
  accessToken: string
  expiresIn: number
  refreshToken: string
}

export type AuthUser = {
  id: string
  claims: Claim[]
}

export type Claim = {
  type: string
  value: string
}

export enum RoleType {
  Administrator = "Administrator",
}