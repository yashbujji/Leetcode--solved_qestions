class Solution:
    def pivotIndex(self, nums: List[int]) -> int:
        aSum = sum(nums)/2
        newSum = 0
        for i in range(len(nums)):
            if newSum + nums[i]/2 == aSum:
                print(newSum + nums[i]/2)
                return i
            newSum = newSum + nums[i]
        
        return -1