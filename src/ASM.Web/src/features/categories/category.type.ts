import { BaseEntity } from "@/types/data"

export type Category = BaseEntity & {
  name: string
  prefix: string
}

export type ListCategories = {
  categories: Category[]
}

export type CreateCategoryRequest = {
  name: string
  prefix: string
}
