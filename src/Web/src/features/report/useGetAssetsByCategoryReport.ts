import { useQuery } from "@tanstack/react-query"

import reportService from "./report.service"
import { AssetsByCategoryRequest } from "./report.type"

export default function useGetAssetsByCategoryReport(
  options?: Partial<AssetsByCategoryRequest>
) {
  return useQuery({
    queryKey: ["assets-by-category", options],
    queryFn: () => reportService.getAssetsByCategoryReport(options),
  })
}
