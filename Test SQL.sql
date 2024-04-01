
--2.1
select a.CITY, SUM(b.AMOUNT) Amount from TB_M_SUPPLIER a
left join TB_R_ORDER_H b on a.SUPPLIER_CODE =  b.SUPPLIER_CODE
group by a.CITY

--2.2
select a.SUPPLIER_NAME, SUM(b.AMOUNT) Amount from TB_M_SUPPLIER a
left join TB_R_ORDER_H b on a.SUPPLIER_CODE =  b.SUPPLIER_CODE
where MONTH(b.ORDER_DATE) = 1 and YEAR(b.ORDER_DATE) = 2019
group by a.SUPPLIER_NAME 

--2.3
select a.SUPPLIER_NAME, max(ORDER_DATE) TransactionDate from TB_M_SUPPLIER a
left join TB_R_ORDER_H b on a.SUPPLIER_CODE =  b.SUPPLIER_CODE
group by a.SUPPLIER_NAME  

--2.4
select a.SUPPLIER_NAME, SUM(b.AMOUNT) TransactionDate from TB_M_SUPPLIER a
left join TB_R_ORDER_H b on a.SUPPLIER_CODE =  b.SUPPLIER_CODE
where PROVINCE = 'Jawa Barat'
group by a.SUPPLIER_NAME  
having SUM(b.AMOUNT) > 30000000

--2.5
select a.SUPPLIER_NAME, SUM(b.AMOUNT) Amount, YEAR(ORDER_DATE) from TB_M_SUPPLIER a
left join TB_R_ORDER_H b on a.SUPPLIER_CODE =  b.SUPPLIER_CODE
where YEAR(b.ORDER_DATE) = 2019
group by a.SUPPLIER_CODE,SUPPLIER_NAME, YEAR(ORDER_DATE)
order by YEAR(ORDER_DATE) desc, a.SUPPLIER_CODE asc
	