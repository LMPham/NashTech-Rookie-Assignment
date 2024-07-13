import { AuthUser } from "@/features/auth/auth.type"
import { atom } from "jotai"

const initialUser: AuthUser = {
  id: "",
  claims: [],
}

export const userInfo = atom<AuthUser | null>(initialUser)
