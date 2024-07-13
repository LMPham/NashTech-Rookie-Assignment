export type BaseEntity = {
    id: number
    created: Date
    createdBy: string
    createdByUserName: string
    lastModified: Date
    lastModifiedBy: string
    lastModifiedByUserName: string
}

export type Product = BaseEntity & {
    name: string
    descriptions: string[]
    details: ProductDetail[]
    customerReviews: CustomerReview[]
    quantity: number
    price: number
    images: Image[]
}

export type ProductDetail = {
    id: number
    productId: number
    name: string
    description: string
}

export type CustomerReview = BaseEntity & {
    score: number
    headline: string
    comment: string
}

export type Image = {
    productId: number
    name: string
    data: Uint8Array
}