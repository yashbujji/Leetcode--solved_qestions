class Solution:
    def maxSubArray(self, nums: List[int]) -> int:
        arr = []
        arr.append(nums[0])
        maxi = arr[0]
        for i in range(1,len(nums)):
            arr.append(max(arr[i-1] + nums[i], nums[i]))
            if arr[i] > maxi:
                maxi = arr[i]
        return maxi
        