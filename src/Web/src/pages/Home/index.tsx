import { BreadcrumbsContext } from "@libs/contexts/BreadcrumbsContext"
import { useContext, useEffect } from "react"

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

  return <>Hello</>
}
