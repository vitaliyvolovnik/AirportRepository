export interface Pageable<T>{
    totalItems?:number
    pageSize?:number
    pageIndex?:number
    result:T[]
}