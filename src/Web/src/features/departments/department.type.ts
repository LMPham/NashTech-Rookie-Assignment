import { BaseEntity } from "@/types/data";
import { Category } from "../categories/category.type";

export type Department = BaseEntity & {
    name: string
    description: string
    categories: Category[]
}