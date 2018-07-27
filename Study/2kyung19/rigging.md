# Blender simple rigging
> 블렌더로 모델링 후 리깅하는 법
> mash와 armature 합치기

### 방법
1. 하단 Add 버튼 > Armature > single bone 
2. edit mode에서 척추(중심)를 먼저 잡아 준 뒤
    * z로 bone 위치 파악 또는
    * 우측 Armature > Display > X-Ray 체크
3. ctrl + 좌클릭 으로 계속 bone을 이음
4. 떨어져 있는 bone(ex. 상반신과 하반신)은 shift+우클릭으로 이을 bone 체크 후 > ctrl+p > Keep Offset 클릭
5. 완성 후 pose mode에서 armature를 모두 선택하고 (단축키 a) object mode로 돌아가 단축키 b를 이용해 메쉬 전체 선택
6. ctrl + p  > with automatic weights 클릭
7. pose mode에서 각 본을 좌클릭 한 후 검토

#### **메쉬와 연동되게 bone을 생성해야 오류가 안남**
> 자세한 확인은 2team_DotheG-ame > Study > 2kyung19 > moleRigging.blend 확인

### 참고
* https://www.youtube.com/watch?v=cp1YRaTZBfw