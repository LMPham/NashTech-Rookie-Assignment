import { useContext, useEffect } from "react"
import useListDepartments from "@/features/departments/useListDepartments"
import { BreadcrumbsContext } from "@libs/contexts/BreadcrumbsContext"

import { RouteItem } from "@/types/data"

const breadcrumb: RouteItem[] = [
  {
    label: "Home",
    to: "/home",
  },
]

export default function Home() {
  const context = useContext(BreadcrumbsContext)

  useEffect(() => {
    context?.setBreadcrumbs(breadcrumb)
  }, [])

  const { data, isLoading: listLoading } = useListDepartments()

  return <>Hello</>
}
