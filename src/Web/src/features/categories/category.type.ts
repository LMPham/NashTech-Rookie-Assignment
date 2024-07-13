import { BaseEntity } from "@/types/data";
import { Product } from "../products/product.type";

export type Category = BaseEntity & {
    name: string
    description: string
    products: Product[]
}