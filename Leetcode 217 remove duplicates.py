from ast import List

#217
class Solution:
    def containsDuplicate(self, nums: List[int]) -> bool:
        # if len(nums) <= 1:
        #     return True 
        nums.sort()
        left = 0
        right =  left + 1
        while right < len(nums):
            if nums[left] == nums[right]:
                return True
            left = left +1
            right = right +1
        return False

